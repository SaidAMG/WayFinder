namespace WayFinder.Models;

public class LocationSearchLog
{
    public int Id { get; set; }
    public int EndLocationId { get; set; }

    public string EndLocationName { get; set; }

    public DateTime DateTime { get; set; }
}
