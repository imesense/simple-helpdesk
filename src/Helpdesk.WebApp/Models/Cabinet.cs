using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helpdesk.WebApp.Models;

[Table("Cabinets")]
public class Cabinet {
    [Key]
    public int CabinetId { get; set; }
    [Required]
    public string? Number { get; set; }

    public int LocationId { get; set; }
    [ForeignKey(nameof(LocationId))]
    public Location? Location { get; set; }

    [InverseProperty(nameof(Ticket.Cabinet))]
    public ICollection<Ticket>? Tickets { get; set; }
}
