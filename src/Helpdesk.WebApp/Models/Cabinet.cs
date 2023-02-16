namespace Helpdesk.WebApp.Models;

public sealed class Cabinet : Entity {
    public string Number { get; set; } = string.Empty;

    public int LocationId { get; set; }
    public Location? Location { get; set; }

    public IList<Ticket> Tickets { get; set; } = new List<Ticket>();
}
