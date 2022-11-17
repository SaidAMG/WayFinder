using Microsoft.AspNetCore.Mvc.Rendering;

namespace WayFinder.Areas.Admin.Models;

public class EventEditModel
{
    public int ID { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    public List<SelectListItem>? CompaniesSelectListItems { get; set; }
}
