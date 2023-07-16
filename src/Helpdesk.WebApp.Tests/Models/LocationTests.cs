using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

using Helpdesk.WebApp.Models;

namespace Helpdesk.WebApp.Tests.Models;

[TestClass]
public class LocationTests {
    [TestMethod]
    public void Location_ShouldHaveTableAttribute_WithValueLocations() {
        var tableAttribute = typeof(Location).GetCustomAttribute<TableAttribute>();
        Assert.IsNotNull(tableAttribute);
        Assert.AreEqual("Locations", tableAttribute?.Name);
    }

    [TestMethod]
    public void LocationId_ShouldHaveKeyAttribute() {
        var property = typeof(Location).GetProperty(nameof(Location.LocationId));
        var attribute = property?.GetCustomAttribute<KeyAttribute>();
        Assert.IsNotNull(attribute);
    }

    [TestMethod]
    public void LocationId_ShouldBeSet() {
        var location = new Location {
            LocationId = 1,
        };
        Assert.AreEqual(1, location.LocationId);
    }

    [TestMethod]
    public void Name_ShouldHaveRequiredAttribute() {
        var property = typeof(Location).GetProperty(nameof(Location.Name));
        var attribute = property?.GetCustomAttribute<RequiredAttribute>();
        Assert.IsNotNull(attribute);
    }

    [TestMethod]
    public void Name_ShouldBeRequired() {
        var location = new Location {
            Name = null,
        };
        Assert.IsFalse(Validator.TryValidateObject(location,
            new ValidationContext(location), null, true));
    }

    [TestMethod]
    public void Address_CanBeNull() {
        var location = new Location {
            Address = null,
        };
        Assert.IsNull(location.Address);
    }

    [TestMethod]
    public void Cabinets_CanBeNull() {
        var location = new Location {
            Cabinets = null,
        };
        Assert.IsNull(location.Cabinets);
    }

    [TestMethod]
    public void Cabinets_ShouldHaveInversePropertyAttribute_WithValueLocation() {
        var property = typeof(Location).GetProperty(nameof(Location.Cabinets));
        var attribute = property?.GetCustomAttribute<InversePropertyAttribute>();
        Assert.IsNotNull(attribute);
        Assert.AreEqual(nameof(Cabinet.Location), attribute.Property);
    }

    [TestMethod]
    public void Cabinets_ShouldBeInitialized() {
        var location = new Location {
            Cabinets = new[] {
                new Cabinet()
            }
        };
        Assert.IsNotNull(location.Cabinets);
    }
}
