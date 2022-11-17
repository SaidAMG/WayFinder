using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WayFinder.Data;
using WayFinder.Models;

namespace WayFinder.Areas.Admin.Controllers;

[Area("Admin")]
public class LocationSearchLogController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<LocationSearchLogController> _logger;

    public LocationSearchLogController(ApplicationDbContext context, ILogger<LocationSearchLogController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        return View(await _context.LocationSearchLogs.AsNoTracking().ToListAsync());
    }
}
