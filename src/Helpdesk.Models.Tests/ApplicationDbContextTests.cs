using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Models.Tests;

[TestClass]
public class ApplicationDbContextTests {
    [TestMethod]
    public void Can_Create_ApplicationDbContext() {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Can_Create_ApplicationDbContext")
            .Options;

        var dbContext = new ApplicationDbContext(options);

        Assert.IsNotNull(dbContext);
    }

    [TestMethod]
    public async Task Can_Save_Location() {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Can_Save_Location")
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
    public async Task Can_Delete_Location() {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Can_Delete_Location")
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
