using System.Diagnostics;
using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;

using Helpdesk.WebApp.Controllers;
using Helpdesk.WebApp.Models;

namespace Helpdesk.WebApp.Tests.Controllers;

[TestClass]
public class HomeControllerTests {
    private Mock<ILogger<HomeController>> _logger = null!;

    private HomeController _controller = null!;

    private Activity _activity = null!;

    [TestInitialize]
    public void Initialize() {
        _logger = new Mock<ILogger<HomeController>>();
        _controller = new HomeController(_logger.Object);
        _activity = new Activity("TestActivity").Start();
    }

    [TestCleanup]
    public void Cleanup() {
        _activity.Stop();
    }

    [TestMethod]
    public void HomeController_Constructor_SetsLogger() {
        var loggerField = _controller.GetType()
            .GetField("_logger", BindingFlags.NonPublic | BindingFlags.Instance);
        var loggerValue = loggerField?.GetValue(_controller);
        Assert.AreEqual(_logger.Object, loggerValue);
    }

    [TestMethod]
    public void HomeController_Index_Get_ReturnsViewWithModels() {
        var actionResult = _controller.Index();
        var viewResult = actionResult as ViewResult;
        Assert.IsNotNull(viewResult);
        Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
    }

    [TestMethod]
    public void HomeController_Error_Get_ReturnsViewResultWithModel() {
        var actionResult = _controller.Error();
        var viewResult = actionResult as ViewResult;
        Assert.IsNotNull(viewResult);
        Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        Assert.IsInstanceOfType(viewResult.Model, typeof(ErrorViewModel));
    }
}
