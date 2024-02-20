using Microsoft.EntityFrameworkCore;

namespace Helpdesk.WebApp.Models;

public interface IHelpdeskDbContext {
    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;

    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    DbSet<Location> Locations { get; set; }
    DbSet<Cabinet> Cabinets { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Status> Statuses { get; set; }
    DbSet<Ticket> Tickets { get; set; }
}
