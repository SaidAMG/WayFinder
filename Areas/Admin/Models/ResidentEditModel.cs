using Microsoft.AspNetCore.Mvc.Rendering;

namespace WayFinder.Areas.Admin.Models;

public class ResidentEditModel
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    
    public int? CompanyID { get; set; }
    //public string Password { get; set; }
    
    public List<SelectListItem>? CompaniesSelectListItems { get; set; }
}
