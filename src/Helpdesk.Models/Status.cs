using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helpdesk.Models;

[Table("Statuses")]
public class Status {
    [Key]
    public int StatusId { get; set; }
    [Required]
    public string? Description { get; set; }

    [InverseProperty(nameof(Ticket.Status))]
    public ICollection<Ticket>? Tickets { get; set; }
}
