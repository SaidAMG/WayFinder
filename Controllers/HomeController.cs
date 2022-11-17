using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using WayFinder.Data;
using WayFinder.Models;

namespace WayFinder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;

        public HomeController(
            ILogger<HomeController> logger,
            ApplicationDbContext context,
            IToastNotification toastNotification
        )
        {
            _logger = logger;
            _context = context;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> WayFinder(WayFinderViewModel? model)
        {
            var companies = await _context.Companies.Include(company => company.Location).ToListAsync();
            var residents = await _context.Residents.Include(resident => resident.Company).ThenInclude(company => company.Location).ToListAsync();
            var wayFinderViewModel = model ?? new WayFinderViewModel { LocationsSelectListItems = new List<SelectListItem>() };

            if (model is { StartLocationInput: { }, EndLocationInput: { } })
            {
                var startLocationId = model.StartLocationInput.Split("_")[0];
                var endLocationId = model.EndLocationInput.Split("_")[0];

                var startLocationIdAsInt = int.Parse(startLocationId);
                var endLocationIdAsInt = int.Parse(endLocationId);
                model.StartLocationId = startLocationIdAsInt;
                model.EndLocationId = endLocationIdAsInt;
                model.StartLocation = await _context.Locations.FindAsync(startLocationIdAsInt);
                model.EndLocation = await _context.Locations.FindAsync(endLocationIdAsInt);
                
                // Log search history into table
                _context.Add(new LocationSearchLog
                {
                    EndLocationId = endLocationIdAsInt, EndLocationName = model.EndLocation.Name, DateTime = DateTime.Now
                });
                await _context.SaveChangesAsync();
            }

            wayFinderViewModel.LocationsSelectListItems ??= new List<SelectListItem>();
            
            if (wayFinderViewModel.LocationsSelectListItems is { Count: < 1 })
            {
                wayFinderViewModel.LocationsSelectListItems.AddRange(companies
                    .Select(company => new SelectListItem(company.Name, company.Location.ID + "_" + company.Name)).ToList());
                wayFinderViewModel.LocationsSelectListItems.AddRange(residents
                    .Select(resident => new SelectListItem(resident.FirstName + " " + resident.LastName, resident.Company.Location.ID + "_" + resident.FirstName + " " + resident.LastName)).ToList());
            }

            return View(wayFinderViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> OpenRegisterGuestModal()
        {
            var events = await _context.Events.ToListAsync();
            
            return PartialView("RegisterForEventModal", new GuestRegisterForEventModel
            {
                EventsSelectListItems = events
                    .Select(event1 => new SelectListItem(event1.Name, event1.ID.ToString())).ToList()
            });
        }
        
        [HttpPost]
        public async Task<IActionResult> RegisterGuest(GuestRegisterForEventModel model)
        {
            if (!ModelState.IsValid) return PartialView("RegisterForEventModal", model);

            var event1 = await _context.Events
                .Include(event1 => event1.Guests)
                .Where(event1 => event1.ID == model.EventId)
                .FirstOrDefaultAsync();
            if (event1 == null) return Problem($"Event {model.EventId} does not exist.");
            
            var guest = await _context.Guests
                .Where(guest => guest.FullName == model.FullName)
                .FirstOrDefaultAsync();
            
            if (guest != null)
            {
                event1.Guests.Add(guest);
                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage($"You've successfully registered yourself as {guest.FullName} for the {event1.Name} event!");
            }
            else
            {
                _context.Add(new Guest { FullName = model.FullName });
                await _context.SaveChangesAsync();
                
                guest = await _context.Guests
                    .Where(guest1 => guest1.FullName == model.FullName)
                    .FirstOrDefaultAsync();
                
                // Check if guest has been successfully added to database...
                if (guest != null)
                {
                    event1.Guests.Add(guest);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage($"You've successfully registered yourself as {guest.FullName} for the {event1.Name} event!");
                }
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
