using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WayFinder.Models;

public class WayFinderViewModel
{
    [DisplayName("Start Location")]
    public string? StartLocationInput { get; set; }
    [DisplayName("End Location")]
    public string? EndLocationInput { get; set; }
    
    public int? StartLocationId { get; set; }
    public int? EndLocationId { get; set; }
    
    public Location? StartLocation { get; set; }
    public Location? EndLocation { get; set; }
    
    public List<SelectListItem>? LocationsSelectListItems { get; set; }

    public override string ToString() => "WayFinderViewModel{" +
                                         "StartLocationInput=" + StartLocationInput + "," +
                                         " EndLocationInput=" + EndLocationInput + "," +
                                         " StartLocationId=" + StartLocationId + "," +
                                         " EndLocationId=" + EndLocationId + "," +
                                         " StartLocation=" + StartLocation + "," +
                                         " EndLocation=" + EndLocation + "," +
                                         " LocationsSelectListItem=" + LocationsSelectListItems + "}";
}
