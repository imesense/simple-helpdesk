using Moq;

using MockQueryable.Moq;

using Helpdesk.WebApp.Models;

namespace Helpdesk.WebApp.Tests.Models;

[TestClass]
public class DbContextMockTests {
    private class TestService {
        public TestService(IHelpdeskDbContext context) => _ = context;
    }

    private Mock<IHelpdeskDbContext> _contextMock = null!;

    [TestInitialize]
    public void Initialize() {
        _contextMock = new Mock<IHelpdeskDbContext>();
    }

    [TestMethod]
    public void DbContext_CanBeCreated() {
        var service = new TestService(Mock.Of<IHelpdeskDbContext>());
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void Locations_MatchesExpectedValue() {
        var locations = new List<Location> {
            new Location(),
        };
        var mockDbSet = locations.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Locations).Returns(mockDbSet.Object);
        CollectionAssert.AreEquivalent(locations,
            _contextMock.Object.Locations.ToList());
    }

    [TestMethod]
    public void Cabinets_MatchesExpectedValue() {
        var cabinets = new List<Cabinet> {
            new Cabinet(),
        };
        var mockDbSet = cabinets.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Cabinets).Returns(mockDbSet.Object);
        CollectionAssert.AreEquivalent(cabinets,
            _contextMock.Object.Cabinets.ToList());
    }

    [TestMethod]
    public void Users_MatchesExpectedValue() {
        var uses = new List<User> {
            new User(),
        };
        var mockDbSet = uses.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Users).Returns(mockDbSet.Object);
        CollectionAssert.AreEquivalent(uses,
            _contextMock.Object.Users.ToList());
    }

    [TestMethod]
    public void Statuses_MatchesExpectedValue() {
        var statuses = new List<Status> {
            new Status(),
        };
        var mockDbSet = statuses.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Statuses).Returns(mockDbSet.Object);
        CollectionAssert.AreEquivalent(statuses,
            _contextMock.Object.Statuses.ToList());
    }

    [TestMethod]
    public void Tickets_MatchesExpectedValue() {
        var tickets = new List<Ticket> {
            new Ticket(),
        };
        var mockDbSet = tickets.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Tickets).Returns(mockDbSet.Object);
        CollectionAssert.AreEquivalent(tickets,
            _contextMock.Object.Tickets.ToList());
    }
}
