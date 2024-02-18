using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using MockQueryable.Moq;

using Moq;

using Helpdesk.WebApp.Controllers;
using Helpdesk.WebApp.Models;

namespace Helpdesk.WebApp.Tests.Controllers;

[TestClass]
public class StatusControllerTests {
    private Mock<ILogger<StatusController>> _logger = null!;
    private Mock<IDbContext> _context = null!;

    private StatusController _controller = null!;

    [TestInitialize]
    public void Initialize() {
        _logger = new Mock<ILogger<StatusController>>();
        _context = new Mock<IDbContext>();
        _controller = new StatusController(_logger.Object, _context.Object);
    }

    [TestMethod]
    public void StatusController_Constructor_SetsLogger() {
        var loggerField = _controller.GetType()
            .GetField("_logger", BindingFlags.NonPublic | BindingFlags.Instance);
        var loggerValue = loggerField?.GetValue(_controller);
        Assert.AreEqual(_logger.Object, loggerValue);
    }

    [TestMethod]
    public void StatusController_Constructor_SetsContext() {
        var contextField = _controller.GetType()
            .GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);
        var contextValue = contextField?.GetValue(_controller);
        Assert.AreEqual(_context.Object, contextValue);
    }

    [TestMethod]
    public async Task StatusController_Index_Get_ReturnsViewWithExistingModels() {
        var statuses = new List<Status> {
            new() {
                StatusId = 1,
                Description = "Status 1",
            },
            new() {
                StatusId = 2,
                Description = "Status 2",
            },
        };
        var mockDbSet = statuses
            .AsQueryable()
            .BuildMockDbSet();
        _context
            .Setup(x => x.Statuses)
            .Returns(mockDbSet.Object);

        var actionResult = await _controller.Index();
        var viewResult = actionResult as ViewResult;
        Assert.IsNotNull(viewResult);
        Assert.IsInstanceOfType(viewResult.Model, typeof(List<Status>));
        Assert.IsInstanceOfType(actionResult, typeof(ViewResult));

        var modelResult = viewResult.ViewData.Model as IEnumerable<Status>;
        Assert.IsInstanceOfType<IEnumerable<Status>>(modelResult);
        Assert.AreEqual(statuses.Count, modelResult.Count());
        Assert.AreEqual(statuses[0].StatusId, modelResult.ElementAt(0).StatusId);
        Assert.AreEqual(statuses[0].Description, modelResult.ElementAt(0).Description);
        Assert.AreEqual(statuses[1].StatusId, modelResult.ElementAt(1).StatusId);
        Assert.AreEqual(statuses[1].Description, modelResult.ElementAt(1).Description);
    }

    [TestMethod]
    public async Task StatusController_Details_Get_ReturnsNotFoundResultWhenModelIsNull() {
        var statuses = new List<Status>();
        var mockDbSet = statuses
            .AsQueryable()
            .BuildMockDbSet();
        _context
            .Setup(x => x.Statuses)
            .Returns(mockDbSet.Object);

        var actionResult = await _controller.Details(0) as NotFoundResult;
        Assert.IsNotNull(actionResult);
        Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task StatusController_Details_Get_ReturnsViewWithExistingModel() {
        var statuses = new List<Status> {
            new() {
                StatusId = 1,
                Description = "Status",
            },
        };
        var mockDbSet = statuses
            .AsQueryable()
            .BuildMockDbSet();
        _context
            .Setup(x => x.Statuses)
            .Returns(mockDbSet.Object);

        var actionResult = await _controller.Details(1);
        var viewResult = actionResult as ViewResult;
        Assert.IsNotNull(viewResult);
        Assert.IsInstanceOfType(viewResult.Model, typeof(Status));
        Assert.IsInstanceOfType(actionResult, typeof(ViewResult));

        var modelResult = viewResult.ViewData.Model as Status;
        Assert.IsInstanceOfType<Status>(modelResult);
        Assert.AreEqual(statuses[0].StatusId, modelResult.StatusId);
        Assert.AreEqual(statuses[0].Description, modelResult.Description);
    }

    [TestMethod]
    public void StatusController_Create_Get_ReturnsViewResult() {
        var result = _controller.Create() as ViewResult;
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task StatusController_Create_Post_ReturnsViewResultWithInvalidModel() {
        Status status = null!;
        _context.Setup(x => x.Statuses.Add(status));
        _context
            .Setup(x => x.SaveChangesAsync(default))
            .ReturnsAsync(1);

        var actionResult = await _controller.Create(status);
        var viewResult = actionResult as ViewResult;
        Assert.IsNotNull(viewResult);
        Assert.IsNull(viewResult.Model);
        Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
    }

    [TestMethod]
    public async Task StatusController_Create_Post_ReturnsRedirectToActionResultWithValidModel() {
        var status = new Status {
            StatusId = 1,
            Description = "Status",
        };
        _context.Setup(x => x.Statuses.Add(status));
        _context
            .Setup(x => x.SaveChangesAsync(default))
            .ReturnsAsync(1);

        var actionResult = await _controller.Create(status);
        var viewResult = actionResult as RedirectToActionResult;
        Assert.IsNotNull(viewResult);
        Assert.IsInstanceOfType(actionResult, typeof(RedirectToActionResult));
        Assert.AreEqual(viewResult.ActionName, nameof(StatusController.Index));
    }

    [TestMethod]
    public async Task StatusController_Edit_Get_ReturnsNotFoundResultWhenModelIsNull() {
        var statuses = new List<Status>();
        var mockDbSet = statuses
            .AsQueryable()
            .BuildMockDbSet();
        _context
            .Setup(x => x.Statuses)
            .Returns(mockDbSet.Object);

        var actionResult = await _controller.Edit(0) as NotFoundResult;
        Assert.IsNotNull(actionResult);
        Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task StatusController_Edit_Get_ReturnsViewWithExistingModel() {
        var statuses = new List<Status> {
            new() {
                StatusId = 1,
                Description = "Status",
            },
        };
        var mockDbSet = statuses
            .AsQueryable()
            .BuildMockDbSet();
        _context
            .Setup(x => x.Statuses)
            .Returns(mockDbSet.Object);

        var actionResult = await _controller.Edit(1);
        var viewResult = actionResult as ViewResult;
        Assert.IsNotNull(viewResult);
        Assert.IsInstanceOfType(viewResult.Model, typeof(Status));
        Assert.IsInstanceOfType(actionResult, typeof(ViewResult));

        var modelResult = viewResult.ViewData.Model as Status;
        Assert.IsInstanceOfType<Status>(modelResult);
        Assert.AreEqual(statuses[0].StatusId, modelResult.StatusId);
        Assert.AreEqual(statuses[0].Description, modelResult.Description);
    }

    [TestMethod]
    public async Task StatusController_Edit_Post_ReturnsViewResultWithInvalidModel() {
        Status status = null!;
        _context.Setup(x => x.Statuses.Add(status));
        _context
            .Setup(x => x.SaveChangesAsync(default))
            .ReturnsAsync(1);

        var actionResult = await _controller.Edit(1, status);
        var viewResult = actionResult as ViewResult;
        Assert.IsNotNull(viewResult);
        Assert.IsNull(viewResult.Model);
        Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
    }

    [TestMethod]
    public async Task StatusController_Edit_Post_ReturnsNotFoundResultWhenModelIsNull() {
        var status = new Status {
            StatusId = 1,
            Description = "Status 1",
        };
        var statuses = new List<Status> {
            new() {
                StatusId = 2,
                Description = "Status 2",
            },
        };
        var mockDbSet = statuses
            .AsQueryable()
            .BuildMockDbSet();
        _context
            .Setup(x => x.Statuses)
            .Returns(mockDbSet.Object);
        _context
            .Setup(x => x.SaveChangesAsync(default))
            .ReturnsAsync(1);

        var actionResult = await _controller.Edit(1, status);
        var viewResult = actionResult as NotFoundResult;
        Assert.IsNotNull(viewResult);
        Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task StatusController_Edit_Post_ReturnsRedirectToActionResultWithValidModel() {
        var status = new Status {
            StatusId = 1,
            Description = "Status 2",
        };
        var statuses = new List<Status> {
            new() {
                StatusId = 1,
                Description = "Status 1",
            },
        };
        var mockDbSet = statuses
            .AsQueryable()
            .BuildMockDbSet();
        _context
            .Setup(x => x.Statuses)
            .Returns(mockDbSet.Object);
        _context
            .Setup(x => x.SaveChangesAsync(default))
            .ReturnsAsync(1);

        var actionResult = await _controller.Edit(1, status);
        var viewResult = actionResult as RedirectToActionResult;
        Assert.IsNotNull(viewResult);
        Assert.IsInstanceOfType(actionResult, typeof(RedirectToActionResult));
        Assert.AreEqual(viewResult.ActionName, nameof(StatusController.Index));
    }

    [TestMethod]
    public async Task StatusController_Delete_Get_ReturnsNotFoundResultWhenModelIsNull() {
        var statuses = new List<Status>();
        var mockDbSet = statuses
            .AsQueryable()
            .BuildMockDbSet();
        _context
            .Setup(x => x.Statuses)
            .Returns(mockDbSet.Object);

        var actionResult = await _controller.Delete(0) as NotFoundResult;
        Assert.IsNotNull(actionResult);
        Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task StatusController_Delete_Get_ReturnsViewWithExistingModel() {
        var statuses = new List<Status> {
            new() {
                StatusId = 1,
                Description = "Status",
            },
        };
        var mockDbSet = statuses
            .AsQueryable()
            .BuildMockDbSet();
        _context
            .Setup(x => x.Statuses)
            .Returns(mockDbSet.Object);

        var actionResult = await _controller.Delete(1);
        var viewResult = actionResult as ViewResult;
        Assert.IsNotNull(viewResult);
        Assert.IsInstanceOfType(viewResult.Model, typeof(Status));
        Assert.IsInstanceOfType(actionResult, typeof(ViewResult));

        var modelResult = viewResult.ViewData.Model as Status;
        Assert.IsInstanceOfType<Status>(modelResult);
        Assert.AreEqual(statuses[0].StatusId, modelResult.StatusId);
        Assert.AreEqual(statuses[0].Description, modelResult.Description);
    }

    [TestMethod]
    public async Task StatusController_Delete_Post_ReturnsViewResultWithInvalidModel() {
        Status status = null!;
        _context.Setup(x => x.Statuses.Add(status));
        _context
            .Setup(x => x.SaveChangesAsync(default))
            .ReturnsAsync(1);

        var actionResult = await _controller.Delete(1, status);
        var viewResult = actionResult as ViewResult;
        Assert.IsNotNull(viewResult);
        Assert.IsNull(viewResult.Model);
        Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
    }

    [TestMethod]
    public async Task StatusController_Delete_Post_ReturnsNotFoundResultWhenModelIsNull() {
        var status = new Status {
            StatusId = 1,
            Description = "Status 1",
        };
        var statuses = new List<Status> {
            new() {
                StatusId = 2,
                Description = "Status 2",
            },
        };
        var mockDbSet = statuses
            .AsQueryable()
            .BuildMockDbSet();
        _context
            .Setup(x => x.Statuses)
            .Returns(mockDbSet.Object);
        _context
            .Setup(x => x.SaveChangesAsync(default))
            .ReturnsAsync(1);

        var actionResult = await _controller.Delete(1, status);
        var viewResult = actionResult as NotFoundResult;
        Assert.IsNotNull(viewResult);
        Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task StatusController_Delete_Post_ReturnsRedirectToActionResultWithValidModel() {
        var status = new Status {
            StatusId = 1,
            Description = "Status 2",
        };
        var statuses = new List<Status> {
            new() {
                StatusId = 1,
                Description = "Status 1",
            },
        };
        var mockDbSet = statuses
            .AsQueryable()
            .BuildMockDbSet();
        _context
            .Setup(x => x.Statuses)
            .Returns(mockDbSet.Object);
        _context
            .Setup(x => x.SaveChangesAsync(default))
            .ReturnsAsync(1);

        var actionResult = await _controller.Delete(1, status);
        var viewResult = actionResult as RedirectToActionResult;
        Assert.IsNotNull(viewResult);
        Assert.IsInstanceOfType(actionResult, typeof(RedirectToActionResult));
        Assert.AreEqual(viewResult.ActionName, nameof(StatusController.Index));
    }
}
