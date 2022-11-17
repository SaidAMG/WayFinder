using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WayFinder.Areas.Admin.Models;

public class EventCreateModel
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    public List<SelectListItem>? CompaniesSelectListItems { get; set; }
}
