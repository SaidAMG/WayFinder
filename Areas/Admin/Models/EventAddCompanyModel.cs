using Microsoft.AspNetCore.Mvc.Rendering;

namespace WayFinder.Areas.Admin.Models;

public class EventAddCompanyModel
{
    public int EventId { get; set; }
    public int CompanyId { get; set; }
    
    public List<SelectListItem>? CompaniesSelectListItems { get; set; }
}
