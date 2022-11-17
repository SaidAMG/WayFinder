using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WayFinder.Areas.Admin.Models;
using WayFinder.Data;
using WayFinder.Models;

namespace WayFinder.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class ResidentsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<Resident> _userManager;
    private readonly IMapper _mapper;

    public ResidentsController(ApplicationDbContext context, UserManager<Resident> userManager,
        IMapper mapper)
    {
        _context = context;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Residents.AsNoTracking().ToListAsync());
    }

    public async Task<IActionResult> Create()
    {
        var companies = await _context.Companies.ToListAsync();

        return PartialView("CreateResidentModal", new ResidentCreateModel
        {
            CompaniesSelectListItems = companies
                .Select(company => new SelectListItem(company.Name, company.ID.ToString())).ToList()
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(ResidentCreateModel model)
    {
        if (!ModelState.IsValid) return BadRequest();

        var resident = new Resident
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.UserName,
            Email = model.Email,
            CompanyID = model.CompanyID,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(resident, model.Password);
        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }

        return PartialView("CreateResidentModal", model);
    }

    [HttpPost]
    public async Task<IActionResult> Details(string? id)
    {
        if (id == null) return NotFound();

        var resident = await _context.Residents
            .Include(resident => resident.Company)
            .Where(resident => resident.Id == id)
            .FirstOrDefaultAsync();
        if (resident == null) return NotFound();
        return PartialView("ViewResidentModal", resident);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null) return NotFound();

        var resident = await _context.Residents.FindAsync(id);
        if (resident == null) return NotFound();
        var companies = await _context.Companies.ToListAsync();
        var residentEditModel = _mapper.Map<ResidentEditModel>(resident);
        residentEditModel.CompaniesSelectListItems = companies
            .Select(company => new SelectListItem(company.Name, company.ID.ToString(), resident.Company == company))
            .ToList();

        return PartialView("EditResidentModal", residentEditModel);
    }

    [HttpPost]
    public async Task<IActionResult> Update(ResidentEditModel model)
    {
        var resident = await _context.Residents.FindAsync(model.Id);
        if (resident == null) return NotFound();
        resident.FirstName = model.FirstName;
        resident.LastName = model.LastName;
        resident.UserName = model.UserName;
        resident.Email = model.Email;
        resident.CompanyID = model.CompanyID;
        //resident.PasswordHash = _passwordHasher.HashPassword(resident, model.Password)

        _context.Update(resident);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null) return NotFound();
        var resident = await _context.Residents.FindAsync(id);
        if (resident == null) return NotFound();

        var result = await _userManager.DeleteAsync(resident);
        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest();
    }
}
