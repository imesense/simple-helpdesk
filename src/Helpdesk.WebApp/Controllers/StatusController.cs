using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Helpdesk.WebApp.Models;

namespace Helpdesk.WebApp.Controllers;

[Route("[controller]")]
public class StatusController : Controller {
    private readonly ILogger<StatusController> _logger;
    private readonly IDbContext _context;

    public StatusController(ILogger<StatusController> logger,
        IDbContext context) {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index() {
        var models = await _context.Statuses.ToListAsync();
        return View(models);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Details(int id) {
        var model = await _context.Statuses
            .Where(x => x.StatusId == id)
            .FirstOrDefaultAsync();
        if (model is not null) {
            return View(model);
        }
        return NotFound();
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult Create() {
        return View();
    }

    [HttpPost]
    [Route("[action]")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Status model) {
        if (model is not null) {
            _context.Statuses.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Edit(int id) {
        var model = await _context.Statuses
            .Where(x => x.StatusId == id)
            .FirstOrDefaultAsync();
        if (model is not null) {
            return View(model);
        }
        return NotFound();
    }

    [HttpPost]
    [Route("[action]/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Status model) {
        if (model is not null) {
            var record = await _context.Statuses
                .Where(x => x.StatusId == id)
                .FirstOrDefaultAsync();
            if (record is null) {
                return NotFound();
            }

            record.Description = model.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Delete(int id) {
        var model = await _context.Statuses
            .Where(x => x.StatusId == id)
            .FirstOrDefaultAsync();
        if (model is not null) {
            return View(model);
        }
        return NotFound();
    }

    [HttpPost]
    [Route("[action]/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, Status model) {
        if (model is not null) {
            var record = await _context.Statuses
                .Where(x => x.StatusId == id)
                .FirstOrDefaultAsync();
            if (record is null) {
                return NotFound();
            }

            _context.Statuses.Remove(record);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}
