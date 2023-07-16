using Helpdesk.WebApp.Models;

namespace Helpdesk.WebApp.Tests.Models;

[TestClass]
public class ErrorViewModelTests {
    [TestMethod]
    public void ShowRequestId_WithValidRequestId_ReturnsTrue() {
        var errorViewModel = new ErrorViewModel {
            RequestId = "12345",
        };
        var showRequestId = errorViewModel.ShowRequestId;
        Assert.IsTrue(showRequestId);
    }

    [TestMethod]
    public void ShowRequestId_WithEmptyRequestId_ReturnsFalse() {
        var errorViewModel = new ErrorViewModel {
            RequestId = "",
        };
        var showRequestId = errorViewModel.ShowRequestId;
        Assert.IsFalse(showRequestId);
    }

    [TestMethod]
    public void ShowRequestId_WithNullRequestId_ReturnsFalse() {
        var errorViewModel = new ErrorViewModel {
            RequestId = null,
        };
        var showRequestId = errorViewModel.ShowRequestId;
        Assert.IsFalse(showRequestId);
    }
}
