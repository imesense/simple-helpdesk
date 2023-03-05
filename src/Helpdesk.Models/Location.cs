using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helpdesk.Models;

[Table("Locations")]
public class Location {
    [Key]
    public int LocationId { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Address { get; set; }

    [InverseProperty(nameof(Cabinet.Location))]
    public ICollection<Cabinet>? Cabinets { get; set; }
}
