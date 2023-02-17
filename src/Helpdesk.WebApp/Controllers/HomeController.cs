using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using Helpdesk.WebApp.Models;

namespace Helpdesk.WebApp.Controllers;

public class HomeController : Controller {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger) {
        _logger = logger;
    }

    [HttpGet]
    [Route("")]
    public IActionResult Index() {
        return View();
    }

    [HttpGet]
    [Route("[action]")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}
