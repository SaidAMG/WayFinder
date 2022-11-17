using Microsoft.AspNetCore.Mvc.Rendering;

namespace WayFinder.Models;

public class GuestRegisterForEventModel
{
    public string FullName { get; set; } = null!;
    public int EventId { get; set; }
    
    public List<SelectListItem>? EventsSelectListItems { get; set; }
}
