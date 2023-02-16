namespace Helpdesk.WebApp.Models;

public sealed class Location : Entity {
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }

    public IList<Cabinet> Cabinets { get; set; } = new List<Cabinet>();
}
