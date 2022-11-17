using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WayFinder.Areas.Admin.Models;
using WayFinder.Models;

namespace WayFinder.Areas.Admin.Controllers;

[Area("Admin")]
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly UserManager<Resident> _userManager;
    private readonly SignInManager<Resident> _signInManager;

    public AccountController(
        ILogger<AccountController> logger,
        UserManager<Resident> userManager,
        SignInManager<Resident> signinManager)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signinManager;
    }

    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        var login = new UserLoginModel { ReturnUrl = returnUrl };

        return View(login);
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLoginModel userLoginModel)
    {
        if (!ModelState.IsValid) return View(userLoginModel);
        var resident = await _userManager.FindByEmailAsync(userLoginModel.Email);
        if (resident != null)
        {
            await _signInManager.SignOutAsync();
            var result = await _signInManager.PasswordSignInAsync(resident, userLoginModel.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToLocal(userLoginModel.ReturnUrl);
            }
        }

        ModelState.AddModelError(nameof(userLoginModel.Email), "UserLoginModel Failed: Invalid Email or password");
        return View(userLoginModel);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home", new { area = "" });
    }

    private IActionResult RedirectToLocal(string? returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return RedirectToAction(nameof(DashboardController.Index), "Dashboard");
    }
}
