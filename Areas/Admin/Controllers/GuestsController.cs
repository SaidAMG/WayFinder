using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WayFinder.Data;

namespace WayFinder.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class GuestsController : Controller
{

    private readonly ILogger _logger;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GuestsController(
        ILogger<GuestsController> logger,
        ApplicationDbContext context,
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IActionResult> Index()
    {
        return View(await _context.Guests.AsNoTracking().ToListAsync());
    }
    
    [HttpPost]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var event1 = await _context.Guests
            .Include(guest => guest.Events)
            .Where(guest => guest.ID == id)
            .FirstOrDefaultAsync();
        if (event1 == null) return NotFound();
        return PartialView("ViewGuestModal", event1);
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var guest = await _context.Guests.FindAsync(id);
        if (guest == null) return NotFound();

        _context.Remove(guest);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
