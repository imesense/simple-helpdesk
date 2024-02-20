using Microsoft.EntityFrameworkCore;

using Helpdesk.WebApp.Models;

namespace Helpdesk.WebApp.Tests.Models;

[TestClass]
public class DbContextIntegrationTests {
    private HelpdeskDbContext _context = null!;

    [TestInitialize]
    public void Initialize() {
        var options = new DbContextOptionsBuilder<HelpdeskDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new HelpdeskDbContext(options);
    }

    [TestCleanup]
    public void Cleanup() {
        _context.Dispose();
    }

    [TestMethod]
    public void ApplicationDbContext_ShouldCreateDatabase() {
        var options = new DbContextOptionsBuilder<HelpdeskDbContext>()
            .UseInMemoryDatabase(databaseName: "CreatedDatabase")
            .Options;
        var context = new HelpdeskDbContext(options);
        Assert.IsNotNull(context);

    }

    [TestMethod]
    public void ApplicationDbContext_ShouldConnectDatabase() {
        Assert.IsTrue(_context.Database.CanConnect());
    }

    [TestMethod]
    public void Locations_ShouldNotBeNull() {
        Assert.IsNotNull(_context.Locations);
    }

    [TestMethod]
    public void Locations_ShouldNotBeEmpty() {
        _context.Locations.Add(new Location {
            Name = "Test Location",
        });
        _context.SaveChanges();
        Assert.AreEqual(1, _context.Locations.Count());
    }

    [TestMethod]
    public void Cabinets_ShouldNotBeNull() {
        Assert.IsNotNull(_context.Cabinets);
    }

    [TestMethod]
    public void Cabinets_ShouldNotBeEmpty() {
        _context.Cabinets.Add(new Cabinet {
            Number = "125",
        });
        _context.SaveChanges();
        Assert.AreEqual(1, _context.Cabinets.Count());
    }

    [TestMethod]
    public void Users_ShouldNotBeNull() {
        Assert.IsNotNull(_context.Users);
    }

    [TestMethod]
    public void Users_ShouldNotBeEmpty() {
        _context.Users.Add(new User {
            Name = "Name",
            Phone = "+7 999 000 11 22",
        });
        _context.SaveChanges();
        Assert.AreEqual(1, _context.Users.Count());
    }

    [TestMethod]
    public void Statuses_ShouldNotBeNull() {
        Assert.IsNotNull(_context.Statuses);
    }

    [TestMethod]
    public void Statuses_ShouldNotBeEmpty() {
        _context.Statuses.Add(new Status {
            Description = "Description",
        });
        _context.SaveChanges();
        Assert.AreEqual(1, _context.Statuses.Count());
    }

    [TestMethod]
    public void Tickets_ShouldNotBeNull() {
        Assert.IsNotNull(_context.Tickets);
    }

    [TestMethod]
    public void Tickets_ShouldNotBeEmpty() {
        _context.Tickets.Add(new Ticket {
            Description = "Description",
        });
        _context.SaveChanges();
        Assert.AreEqual(1, _context.Tickets.Count());
    }
}
