using System.ComponentModel.DataAnnotations;

namespace WayFinder.Models;

public class Level
{
    [Key]
    public int ID { get; set; }
    [Required]
    public int LevelNumber { get; set; }
    public List<Location>? Locations { get; set; }
}
