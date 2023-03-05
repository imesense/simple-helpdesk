using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helpdesk.Models;

[Table("Tickets")]
public class Ticket {
    [Key]
    public int TicketId { get; set; }
    [Required, DataType(DataType.Date)]
    public DateTime Date { get; set; }
    [Required]
    public string? Description { get; set; }

    public int StatusId { get; set; }
    [ForeignKey(nameof(StatusId))]
    public Status? Status { get; set; }

    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    public int CabinetId { get; set; }
    [ForeignKey(nameof(CabinetId))]
    public Cabinet? Cabinet { get; set; }
}
