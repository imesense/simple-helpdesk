using Microsoft.EntityFrameworkCore;

namespace Helpdesk.WebApp.Models;

public class HelpdeskDbContext : DbContext, IHelpdeskDbContext {
    public DbSet<Location> Locations { get; set; }
    public DbSet<Cabinet> Cabinets { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    public HelpdeskDbContext(DbContextOptions<HelpdeskDbContext> options)
        : base(options) {
        Database.EnsureCreated();
    }
}
