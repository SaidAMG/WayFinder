using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WayFinder.Areas.Admin.Models;
using WayFinder.Data;
using WayFinder.Models;

namespace WayFinder.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class LevelsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public LevelsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        return View(await _context.Levels.AsNoTracking().ToListAsync());
    }
    
    
    public IActionResult Create()
    {
        return PartialView("CreateLevelModal", null);
    }

    [HttpPost]
    public async Task<IActionResult> Create(LevelCreateModel model)
    {
        if (!ModelState.IsValid) return PartialView("CreateLevelModal", model);

        var level = _mapper.Map<LevelCreateModel, Level>(model);
        _context.Add(level);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var level = await _context.Levels.FindAsync(id);
        if (level == null) return NotFound();
        return PartialView("ViewLevelModal", level);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var level = await _context.Levels.FindAsync(id);
        if (level == null) return NotFound();
        var levelEditModel = _mapper.Map<LevelEditModel>(level);

        return PartialView("EditLevelModal", levelEditModel);
    }

    [HttpPost]
    public async Task<IActionResult> Update(LevelEditModel model)
    {
        var level = _mapper.Map<Level>(model);

        _context.Update(level);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var level = await _context.Levels.FindAsync(id);
        if (level == null) return NotFound();

        _context.Remove(level);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
