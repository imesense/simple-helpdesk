using System.ComponentModel.DataAnnotations;

using Helpdesk.WebApp.Models;

namespace Helpdesk.WebApp.Tests.Models;

[TestClass]
public class LocationTests {
    [TestMethod]
    public void Location_LocationId_Should_Be_Set() {
        var location = new Location {
            LocationId = 1,
        };
        Assert.AreEqual(1, location.LocationId);
    }

    [TestMethod]
    public void Location_Name_Should_Be_Required() {
        var location = new Location {
            Name = null,
        };
        Assert.IsFalse(Validator.TryValidateObject(location,
            new ValidationContext(location), null, true));
    }

    [TestMethod]
    public void Location_Address_Can_Be_Null() {
        var location = new Location {
            Address = null,
        };
        Assert.IsNull(location.Address);
    }

    [TestMethod]
    public void Location_Cabinets_Can_Be_Null() {
        var location = new Location {
            Cabinets = null,
        };
        Assert.IsNull(location.Cabinets);
    }

    [TestMethod]
    public void Location_Cabinets_Should_Be_Initialized() {
        var location = new Location {
            Cabinets = new[] {
                new Cabinet()
            }
        };
        Assert.IsNotNull(location.Cabinets);
    }
}
