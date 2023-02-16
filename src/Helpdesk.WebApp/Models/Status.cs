namespace Helpdesk.WebApp.Models;

public sealed class Status : Entity {
    public string Description { get; set; } = string.Empty;

    public IList<Ticket> Tickets { get; set; } = new List<Ticket>();
}
