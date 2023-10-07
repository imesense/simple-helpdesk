using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Helpdesk.WebApp.Models;

namespace Helpdesk.WebApp.Controllers;

[Route("[controller]")]
public class CabinetController : Controller {
    private readonly ILogger<CabinetController> _logger;
    private readonly IDbContext _context;

    public CabinetController(ILogger<CabinetController> logger,
        IDbContext context) {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index() {
        var models = await _context.Cabinets
            .Include(c => c.Location)
            .ToListAsync();
        return View(models);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Details(int id) {
        var model = await _context.Cabinets
            .Include(c => c.Location)
            .Where(x => x.CabinetId == id)
            .FirstOrDefaultAsync();
        if (model is not null) {
            return View(model);
        }
        return NotFound();
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> Create() {
        ViewData["Locations"] = new SelectList(await _context.Locations
            .ToListAsync(),
            nameof(Location.LocationId),
            nameof(Location.Name));
        return View();
    }

    [HttpPost]
    [Route("[action]")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Cabinet model) {
        if (model is not null) {
            _context.Cabinets.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Edit(int id) {
        ViewData["Locations"] = new SelectList(await _context.Locations
            .ToListAsync(),
            nameof(Location.LocationId),
            nameof(Location.Name));
        var model = await _context.Cabinets
            .Include(c => c.Location)
            .Where(x => x.CabinetId == id)
            .FirstOrDefaultAsync();
        if (model is not null) {
            return View(model);
        }
        return NotFound();
    }

    [HttpPost]
    [Route("[action]/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Cabinet model) {
        if (model is not null) {
            var record = await _context.Cabinets
                .Include(c => c.Location)
                .Where(x => x.CabinetId == id)
                .FirstOrDefaultAsync();
            if (record is null) {
                return NotFound();
            }

            record.Number = model.Number;
            record.LocationId = model.LocationId;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Delete(int id) {
        ViewData["Locations"] = new SelectList(await _context.Locations
            .ToListAsync(),
            nameof(Location.LocationId),
            nameof(Location.Name));
        var model = await _context.Cabinets
            .Include(c => c.Location)
            .Where(x => x.CabinetId == id)
            .FirstOrDefaultAsync();
        if (model is not null) {
            return View(model);
        }
        return NotFound();
    }

    [HttpPost]
    [Route("[action]/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, Cabinet model) {
        if (model is not null) {
            var record = await _context.Cabinets
                .Include(c => c.Location)
                .Where(x => x.CabinetId == id)
                .FirstOrDefaultAsync();
            if (record is null) {
                return NotFound();
            }

            _context.Cabinets.Remove(record);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}
