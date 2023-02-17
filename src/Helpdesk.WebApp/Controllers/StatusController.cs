using Microsoft.AspNetCore.Mvc;

namespace Helpdesk.WebApp.Controllers;

[Route("[controller]")]
public class StatusController : Controller {
    [HttpGet]
    [Route("")]
    public IActionResult Index() {
        return View();
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public IActionResult Details(int id) {
        return View();
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult Create() {
        return View();
    }

    [HttpPost]
    [Route("[action]")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(IFormCollection collection) {
        try {
            return RedirectToAction(nameof(Index));
        } catch {
            return View();
        }
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public IActionResult Edit(int id) {
        return View();
    }

    [HttpPost]
    [Route("[action]/{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, IFormCollection collection) {
        try {
            return RedirectToAction(nameof(Index));
        } catch {
            return View();
        }
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public IActionResult Delete(int id) {
        return View();
    }

    [HttpPost]
    [Route("[action]/{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id, IFormCollection collection) {
        try {
            return RedirectToAction(nameof(Index));
        } catch {
            return View();
        }
    }
}
