using System.ComponentModel.DataAnnotations;

namespace WayFinder.Models;

public class Company
{
    [Key]
    public int ID { get; set; }

    [Required] public string Name { get; set; } = null!;
    public int? LocationID { get; set; }
    public Location? Location { get; set; }
    public ICollection<Event> Events { get; set; } = null!;
    public ICollection<Resident> Residents { get; set; } = null!;

    public override string ToString() => "Company{ID=" + ID + ", Name=" + Name + ", Location=" + Location + "}";
}
