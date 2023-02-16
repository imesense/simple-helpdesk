namespace Helpdesk.WebApp.Models;

public sealed class User : Entity {
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Position { get; set; }

    public IList<Ticket> Tickets { get; set; } = new List<Ticket>();
}
