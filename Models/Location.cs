using System.ComponentModel.DataAnnotations;

namespace WayFinder.Models;

public class Location
{
    [Key]
    public int ID { get; set; }

    [Required] public string Name { get; set; } = null!;
    public string Description { get; set; }

    public int X { get; set; }
    public int Y { get; set; }
    
    public int LevelID { get; set; }
    public Level? Level { get; set; }
    public List<Company>? Company { get; set; }

}
