using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WayFinder.Areas.Admin.Models;

public class LocationCreateModel
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    public int X { get; set; }
    public int Y { get; set; }
    
    [DisplayName("Level")]
    public int LevelID { get; set; }
    
    public List<SelectListItem>? LevelsSelectListItems { get; set; }
}
