using System.ComponentModel.DataAnnotations;

namespace WayFinder.Models;

public class Guest
{
    [Key]
    public int ID { get; set; }
    [Required]
    public string FullName { get; set; }

    public ICollection<Event> Events { get; set; } = null!;
}
