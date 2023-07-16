using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

using Helpdesk.WebApp.Models;

namespace Helpdesk.WebApp.Tests.Models;

[TestClass]
public class CabinetTests {
    [TestMethod]
    public void Cabinet_ShouldHaveTableAttribute_WithValueLocations() {
        var tableAttribute = typeof(Cabinet).GetCustomAttribute<TableAttribute>();
        Assert.IsNotNull(tableAttribute);
        Assert.AreEqual("Cabinets", tableAttribute?.Name);
    }

    [TestMethod]
    public void CabinetId_ShouldHaveKeyAttribute() {
        var property = typeof(Cabinet).GetProperty(nameof(Cabinet.CabinetId));
        var attribute = property?.GetCustomAttribute<KeyAttribute>();
        Assert.IsNotNull(attribute);
    }

    [TestMethod]
    public void CabinetId_ShouldBeSet() {
        var cabinet = new Cabinet {
            CabinetId = 1,
        };
        Assert.AreEqual(1, cabinet.CabinetId);
    }

    [TestMethod]
    public void Number_ShouldHaveRequiredAttribute() {
        var property = typeof(Cabinet).GetProperty(nameof(Cabinet.Number));
        var attribute = property?.GetCustomAttribute<RequiredAttribute>();
        Assert.IsNotNull(attribute);
    }

    [TestMethod]
    public void Number_ShouldBeRequired() {
        var cabinet = new Cabinet {
            Number = null
        };
        Assert.IsFalse(Validator.TryValidateObject(cabinet,
            new ValidationContext(cabinet), null, true));
    }

    [TestMethod]
    public void LocationId_ShouldBeSetCorrectly() {
        var cabinet = new Cabinet {
            LocationId = 1
        };
        Assert.AreEqual(1, cabinet.LocationId);
    }

    [TestMethod]
    public void Location_ShouldHaveForeignKeyAttribute_WithValueLocationId() {
        var property = typeof(Cabinet).GetProperty(nameof(Cabinet.Location));
        var attribute = property?.GetCustomAttribute<ForeignKeyAttribute>();
        Assert.IsNotNull(attribute);
        Assert.AreEqual("LocationId", attribute?.Name);
    }

    [TestMethod]
    public void Location_ShouldNotBeNull() {
        var cabinet = new Cabinet {
            Location = new Location()
        };
        Assert.IsNotNull(cabinet.Location);
    }

    [TestMethod]
    public void Tickets_ShouldHaveInversePropertyAttribute_WithValueCabinet() {
        var property = typeof(Cabinet).GetProperty(nameof(Cabinet.Tickets));
        var attribute = property?.GetCustomAttribute<InversePropertyAttribute>();
        Assert.IsNotNull(attribute);
        Assert.AreEqual("Cabinet", attribute?.Property);
    }

    [TestMethod]
    public void Tickets_CanBeNull() {
        var cabinet = new Cabinet {
            Tickets = null
        };
        Assert.IsNull(cabinet.Tickets);
    }

    [TestMethod]
    public void Tickets_ShouldBeInitialized() {
        var cabinet = new Cabinet {
            Tickets = new[] {
                new Ticket(),
            },
        };
        Assert.IsNotNull(cabinet.Tickets);
    }
}
