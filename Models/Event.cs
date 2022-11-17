using System.ComponentModel.DataAnnotations;

namespace WayFinder.Models;

public class Event
{
    [Key]
    public int ID { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Company> Companies { get; set; } = null!;
    public ICollection<Guest> Guests { get; set; } = null!;
}
