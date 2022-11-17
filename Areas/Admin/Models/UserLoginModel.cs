using Microsoft.Build.Framework;

namespace WayFinder.Areas.Admin.Models;

public class UserLoginModel
{
    [Required] public string Email { get; set; } = null!;

    [Required] public string Password { get; set; } = null!;
 
    public string? ReturnUrl { get; set; }

    public override string ToString() => "UserLoginModel{Email=" + Email + ", Password=" + Password + "}";
}
