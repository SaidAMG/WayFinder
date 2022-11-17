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
public class CompaniesController : Controller
{
    private readonly ILogger _logger;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CompaniesController(
        ILogger<CompaniesController> logger,
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
        return View(await _context.Companies.AsNoTracking().ToListAsync());
    }

    public async Task<IActionResult> Create()
    {
        var locations = await _context.Locations.ToListAsync();

        return PartialView("CreateCompanyModal",
            new CompanyCreateModel
            {
                LocationsSelectListItems = locations
                    .Select(location => new SelectListItem(location.Name, location.ID.ToString())).ToList()
            });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CompanyCreateModel model)
    {
        if (!ModelState.IsValid) return BadRequest();

        var company = _mapper.Map<CompanyCreateModel, Company>(model);
        _context.Add(company);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var company = await _context.Companies
            .Include(company => company.Location)
            .Include(company => company.Residents)
            .Include(company => company.Events)
            .Where(company => company.ID == id)
            .FirstOrDefaultAsync();
        if (company == null) return NotFound();
        return PartialView("ViewCompanyModal", company);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var company = await _context.Companies.FindAsync(id);
        if (company == null) return NotFound();
        var locations = await _context.Locations.ToListAsync();
        var companyEditModel = _mapper.Map<CompanyEditModel>(company);

        companyEditModel.LocationsSelectListItems = locations
            .Select(location => new SelectListItem(location.Name, location.ID.ToString(), location == company.Location))
            .ToList();

        return PartialView("EditCompanyModal", companyEditModel);
    }

    [HttpPost]
    public async Task<IActionResult> Update(CompanyEditModel model)
    {
        var company = _mapper.Map<Company>(model);
        _logger.LogInformation("CompaniesController - Update - company: {Company}", company);

        _context.Update(company);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var company = await _context.Companies.FindAsync(id);
        if (company == null) return NotFound();

        _context.Remove(company);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
