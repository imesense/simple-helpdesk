using Microsoft.EntityFrameworkCore;

namespace Helpdesk.WebApp.Models;

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        Database.EnsureCreated();
    }
}
