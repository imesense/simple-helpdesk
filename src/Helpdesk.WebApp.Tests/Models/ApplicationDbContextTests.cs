using Microsoft.EntityFrameworkCore;

using Helpdesk.WebApp.Models;

namespace Helpdesk.WebApp.Tests.Models;

[TestClass]
public class ApplicationDbContextTests {
    [TestMethod]
    public void DbContext_Can_Be_Created() {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "DbContext_Can_Be_Created")
            .Options;

        var dbContext = new ApplicationDbContext(options);

        Assert.IsNotNull(dbContext);
    }

    [TestMethod]
    public async Task Location_Can_Be_Saved() {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Location_Can_Be_Saved")
            .Options;
        var location = new Location {
            Name = "Test Location",
            Address = "123 Main St.",
        };

        using (var dbContext = new ApplicationDbContext(options)) {
            dbContext.Locations.Add(location);
            await dbContext.SaveChangesAsync();
        }
        using (var dbContext = new ApplicationDbContext(options)) {
            var result = await dbContext.Locations.FindAsync(location.LocationId);
            Assert.AreEqual("Test Location", result!.Name);
        }
    }

    [TestMethod]
    public async Task Location_Can_Be_Deleted() {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Location_Can_Be_Deleted")
            .Options;
        var location = new Location {
            Name = "Test Location",
            Address = "123 Main St."
        };

        using (var dbContext = new ApplicationDbContext(options)) {
            dbContext.Locations.Add(location);
            await dbContext.SaveChangesAsync();
        }
        using (var dbContext = new ApplicationDbContext(options)) {
            dbContext.Locations.Remove(location);
            await dbContext.SaveChangesAsync();
        }

        using (var dbContext = new ApplicationDbContext(options)) {
            var result = await dbContext.Locations.FindAsync(location.LocationId);
            Assert.IsNull(result);
        }
    }
}
