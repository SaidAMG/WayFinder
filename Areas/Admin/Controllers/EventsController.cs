using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WayFinder.Areas.Admin.Models;
using WayFinder.Data;
using WayFinder.Models;

namespace WayFinder.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class EventsController : Controller
{
    private readonly ILogger _logger;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EventsController(
        ILogger<EventsController> logger,
        ApplicationDbContext context,
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        return View(await _context.Events.AsNoTracking().ToListAsync());
    }

    public IActionResult Create()
    {
        //var companies = await _context.Companies.ToListAsync();

        return PartialView("CreateEventModal", null);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EventCreateModel model)
    {
        if (!ModelState.IsValid) return BadRequest();

        var event1 = _mapper.Map<EventCreateModel, Event>(model);
        _context.Add(event1);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var event1 = await _context.Events
            .Include(company => company.Companies)
            .Include(company => company.Guests)
            .Where(company => company.ID == id)
            .FirstOrDefaultAsync();
        if (event1 == null) return NotFound();
        return PartialView("ViewEventModal", event1);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var event1 = await _context.Events.FindAsync(id);
        if (event1 == null) return NotFound();
        var companies = await _context.Companies.ToListAsync();
        var eventEditModel = _mapper.Map<EventEditModel>(event1);

        /*eventEditModel.LocationsSelectListItems = locations
            .Select(location => new SelectListItem(location.Name, location.ID.ToString(), location == event1.Location)).ToList();*/

        return PartialView("EditEventModal", eventEditModel);
    }

    [HttpPost]
    public async Task<IActionResult> Update(EventEditModel model)
    {
        var event1 = _mapper.Map<Event>(model);

        _context.Update(event1);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var event1 = await _context.Events.FindAsync(id);
        if (event1 == null) return NotFound();

        _context.Remove(event1);
        await _context.SaveChangesAsync();

        return Ok();
    }

    public async Task<IActionResult> OpenAddCompanyToEventModal(int? id)
    {
        if (id == null) return Problem();
        var event1 = await _context.Events
            .Include(event1 => event1.Companies)
            .Where(event1 => event1.ID == id)
            .FirstOrDefaultAsync();
        if (event1 == null) return Problem();
        var companies = await _context.Companies.ToListAsync();
        
        return PartialView("AddMemberEventModal", new EventAddCompanyModel
        {
            EventId = (int)id,
            
            // Don't show companies that are already added to the event.
            CompaniesSelectListItems = companies
                .Where(company => !event1.Companies.Contains(company))
                .Select(company => new SelectListItem(company.Name, company.ID.ToString())).ToList()
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddCompanyToEvent(EventAddCompanyModel? model)
    {
        if (model == null) return NotFound();
        var company = await _context.Companies
            .Include(company1 => company1.Events)
            .Where(company1 => company1.ID == model.CompanyId)
            .FirstOrDefaultAsync();
        if (company == null) return NotFound();
        var event1 = await _context.Events.FindAsync(model.EventId);
        if (event1 == null) return NotFound();

        company.Events.Add(event1);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
