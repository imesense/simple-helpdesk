using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helpdesk.Models;

[Table("Users")]
public class User {
    [Key]
    public int UserId { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Phone { get; set; }
    public string? Position { get; set; }

    [InverseProperty(nameof(Ticket.User))]
    public ICollection<Ticket>? Tickets { get; set; }
}
