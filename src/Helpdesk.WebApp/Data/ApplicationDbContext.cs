using Microsoft.EntityFrameworkCore;

namespace Helpdesk.WebApp.Data;

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        Database.EnsureCreated();
    }
}
