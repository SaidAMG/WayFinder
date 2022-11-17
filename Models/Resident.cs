using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WayFinder.Models;

public class Resident : IdentityUser
{
    [Required] public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? CompanyID { get; set; }
    public Company? Company { get; set; }
}
