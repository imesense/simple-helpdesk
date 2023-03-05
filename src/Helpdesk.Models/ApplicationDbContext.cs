using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Models;

public class ApplicationDbContext : DbContext {
    public DbSet<Location> Locations { get; set; }
    public DbSet<Cabinet> Cabinets { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        Database.EnsureCreated();
    }
}
