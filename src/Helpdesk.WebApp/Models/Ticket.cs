namespace Helpdesk.WebApp.Models;

public sealed class Ticket : Entity {
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;

    public int StatusId { get; set; }
    public Status? Status { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public int CabinetId { get; set; }
    public Cabinet? Cabinet { get; set; }
}
