using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WayFinder.Areas.Admin.Models;

public class CompanyEditModel
{
    public int ID { get; set; }
    public string Name { get; set; } = null!;
    [DisplayName("Location")]
    public int LocationID { get; set; }
    
    public List<SelectListItem>? LocationsSelectListItems { get; set; }
}
