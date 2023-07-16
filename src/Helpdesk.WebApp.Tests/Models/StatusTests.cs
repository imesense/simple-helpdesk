using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

using Helpdesk.WebApp.Models;

namespace Helpdesk.WebApp.Tests.Models;

[TestClass]
public class StatusTests {
    [TestMethod]
    public void Status_ShouldHaveTableAttribute_WithValueStatuses() {
        var tableAttribute = typeof(Status).GetCustomAttribute<TableAttribute>();
        Assert.IsNotNull(tableAttribute);
        Assert.AreEqual("Statuses", tableAttribute?.Name);
    }

    [TestMethod]
    public void StatusId_ShouldHaveKeyAttribute() {
        var property = typeof(Status).GetProperty(nameof(Status.StatusId));
        var attribute = property?.GetCustomAttribute<KeyAttribute>();
        Assert.IsNotNull(attribute);
    }

    [TestMethod]
    public void StatusId_ShouldBeSet() {
        var status = new Status {
            StatusId = 1,
        };
        Assert.AreEqual(1, status.StatusId);
    }

    [TestMethod]
    public void Description_ShouldHaveRequiredAttribute() {
        var property = typeof(Status).GetProperty(nameof(Status.Description));
        var attribute = property?.GetCustomAttribute<RequiredAttribute>();
        Assert.IsNotNull(attribute);
    }

    [TestMethod]
    public void Description_ShouldBeRequired() {
        var status = new Status {
            Description = null,
        };
        Assert.IsFalse(Validator.TryValidateObject(status,
            new ValidationContext(status), null, true));
    }

    [TestMethod]
    public void Description_CanBeNull() {
        var status = new Status {
            Description = null,
        };
        Assert.IsNull(status.Description);
    }

    [TestMethod]
    public void Description_ShouldBeSet() {
        var status = new Status {
            Description = "Description",
        };
        Assert.AreEqual("Description", status.Description);
    }

    [TestMethod]
    public void Tickets_CanBeNull() {
        var status = new Status {
            Tickets = null,
        };
        Assert.IsNull(status.Tickets);
    }

    [TestMethod]
    public void Tickets_ShouldBeInitialized() {
        var status = new Status {
            Tickets = new[] {
                new Ticket(),
            },
        };
        Assert.IsNotNull(status.Tickets);
    }
}
