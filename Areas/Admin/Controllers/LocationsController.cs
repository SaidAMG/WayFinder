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
public class LocationsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public LocationsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        return View(await _context.Locations.Include(location => location.Level).AsNoTracking().ToListAsync());
    }
    
    public async Task<IActionResult> Create()
    {
        var levels = await _context.Levels.ToListAsync();
        
        return PartialView("CreateLocationModal", new LocationCreateModel
        {
            LevelsSelectListItems = levels
                .Select(location => new SelectListItem(location.LevelNumber.ToString(), location.ID.ToString())).ToList()
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(LocationCreateModel model)
    {
        if (!ModelState.IsValid) return PartialView("CreateLocationModal", model);

        var location = _mapper.Map<LocationCreateModel, Location>(model);
        _context.Add(location);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var location = await _context.Locations
            .Include(location => location.Level)
            .Where(location => location.ID == id)
            .FirstOrDefaultAsync();
        if (location == null) return NotFound();
        return PartialView("ViewLocationModal", location);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var location = await _context.Locations.FindAsync(id);
        if (location == null) return NotFound();
        var levels = await _context.Levels.ToListAsync();
        var locationEditModel = _mapper.Map<LocationEditModel>(location);
        locationEditModel.LevelsSelectListItems = levels.Select(level =>
            new SelectListItem(level.LevelNumber.ToString(), level.ID.ToString(), location.Level == level)).ToList(); 

        return PartialView("EditLocationModal", locationEditModel);
    }

    [HttpPost]
    public async Task<IActionResult> Update(LocationEditModel model)
    {
        var location = _mapper.Map<Location>(model);

        _context.Update(location);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var location = await _context.Locations.FindAsync(id);
        if (location == null) return NotFound();

        _context.Remove(location);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
