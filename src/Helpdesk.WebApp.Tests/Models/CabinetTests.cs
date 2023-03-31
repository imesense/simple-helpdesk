using System.ComponentModel.DataAnnotations;

using Helpdesk.WebApp.Models;

namespace Helpdesk.WebApp.Tests.Models;

[TestClass]
public class CabinetTests {
    [TestMethod]
    public void Cabinet_CabinetId_Should_Be_Set() {
        var expectedCabinetId = 1;
        var cabinet = new Cabinet {
            CabinetId = 1,
        };
        Assert.AreEqual(expectedCabinetId, cabinet.CabinetId);
    }

    [TestMethod]
    public void Cabinet_Number_Should_Be_Required() {
        var cabinet = new Cabinet {
            Number = null
        };
        Assert.IsFalse(Validator.TryValidateObject(cabinet,
            new ValidationContext(cabinet), null, true));
    }

    [TestMethod]
    public void Cabinet_LocationId_Should_Be_Set_Correctly() {
        var expectedLocationId = 1;

        var cabinet = new Cabinet {
            LocationId = 1
        };
        var actualLocationId = cabinet.LocationId;

        Assert.AreEqual(expectedLocationId, actualLocationId);
    }

    [TestMethod]
    public void Cabinet_Location_Should_Not_Be_Null() {
        var cabinet = new Cabinet {
            Location = new Location()
        };
        Assert.IsNotNull(cabinet.Location);
    }

    [TestMethod]
    public void Cabinet_Tickets_Can_Be_Null() {
        var cabinet = new Cabinet {
            Tickets = null
        };
        Assert.IsNull(cabinet.Tickets);
    }

    [TestMethod]
    public void Cabinet_Tickets_Should_Be_Initialized() {
        var cabinet = new Cabinet {
            Tickets = new[] {
                new Ticket(),
            },
        };
        Assert.IsNotNull(cabinet.Tickets);
    }
}
