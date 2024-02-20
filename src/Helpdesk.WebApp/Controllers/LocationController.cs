using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Helpdesk.WebApp.Models;

namespace Helpdesk.WebApp.Controllers;

[Route("[controller]")]
public class LocationController : Controller {
    private readonly ILogger<LocationController> _logger;
    private readonly IHelpdeskDbContext _context;

    public LocationController(ILogger<LocationController> logger,
        IHelpdeskDbContext context) {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index() {
        var models = await _context.Locations.ToListAsync();
        return View(models);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Details(int id) {
        var model = await _context.Locations
            .Where(x => x.LocationId == id)
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
    public async Task<IActionResult> Create(Location model) {
        if (model is not null) {
            _context.Locations.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Edit(int id) {
        var model = await _context.Locations
            .Where(x => x.LocationId == id)
            .FirstOrDefaultAsync();
        if (model is not null) {
            return View(model);
        }
        return NotFound();
    }

    [HttpPost]
    [Route("[action]/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Location model) {
        if (model is not null) {
            var record = await _context.Locations
                .Where(x => x.LocationId == id)
                .FirstOrDefaultAsync();
            if (record is null) {
                return NotFound();
            }

            record.Name = model.Name;
            record.Address = model.Address;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Delete(int id) {
        var model = await _context.Locations
            .Where(x => x.LocationId == id)
            .FirstOrDefaultAsync();
        if (model is not null) {
            return View(model);
        }
        return NotFound();
    }

    [HttpPost]
    [Route("[action]/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, Location model) {
        if (model is not null) {
            var record = await _context.Locations
                .Where(x => x.LocationId == id)
                .FirstOrDefaultAsync();
            if (record is null) {
                return NotFound();
            }

            _context.Locations.Remove(record);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}
