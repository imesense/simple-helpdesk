using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Helpdesk.WebApp.Models;

using UserModel = Helpdesk.WebApp.Models.User;

namespace Helpdesk.WebApp.Controllers;

[Route("[controller]")]
public class TicketController : Controller {
    private readonly ILogger<TicketController> _logger;
    private readonly IHelpdeskDbContext _context;

    public TicketController(ILogger<TicketController> logger,
        IHelpdeskDbContext context) {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index() {
        var models = await _context.Tickets
            .Include(c => c.Status)
            .Include(c => c.User)
            .Include(c => c.Cabinet)
            .Include(c => c.Cabinet!.Location)
            .ToListAsync();
        return View(models);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Details(int id) {
        var model = await _context.Tickets
            .Include(c => c.Status)
            .Include(c => c.User)
            .Include(c => c.Cabinet)
            .Include(c => c.Cabinet!.Location)
            .Where(x => x.TicketId == id)
            .FirstOrDefaultAsync();
        if (model is not null) {
            return View(model);
        }
        return NotFound();
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> Create() {
        ViewData["Statuses"] = new SelectList(await _context.Statuses
            .ToListAsync(),
            nameof(Status.StatusId),
            nameof(Status.Description));
        ViewData["Users"] = new SelectList(await _context.Users
            .ToListAsync(),
            nameof(UserModel.UserId),
            nameof(UserModel.Name));
        ViewData["Cabinets"] = new SelectList(await _context.Cabinets
            .ToListAsync(),
            nameof(Cabinet.CabinetId),
            nameof(Cabinet.Number));
        return View();
    }

    [HttpPost]
    [Route("[action]")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Ticket model) {
        if (model is not null) {
            _context.Tickets.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Edit(int id) {
        ViewData["Statuses"] = new SelectList(await _context.Statuses
            .ToListAsync(),
            nameof(Status.StatusId),
            nameof(Status.Description));
        ViewData["Users"] = new SelectList(await _context.Users
            .ToListAsync(),
            nameof(UserModel.UserId),
            nameof(UserModel.Name));
        ViewData["Cabinets"] = new SelectList(await _context.Cabinets
            .ToListAsync(),
            nameof(Cabinet.CabinetId),
            nameof(Cabinet.Number));
        var model = await _context.Tickets
            .Include(c => c.Status)
            .Include(c => c.User)
            .Include(c => c.Cabinet)
            .Where(x => x.TicketId == id)
            .FirstOrDefaultAsync();
        if (model is not null) {
            return View(model);
        }
        return NotFound();
    }

    [HttpPost]
    [Route("[action]/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Ticket model) {
        if (model is not null) {
            var record = await _context.Tickets
                .Include(c => c.Status)
                .Include(c => c.User)
                .Include(c => c.Cabinet)
                .Where(x => x.TicketId == id)
                .FirstOrDefaultAsync();
            if (record is null) {
                return NotFound();
            }

            record.Date = model.Date;
            record.Description = model.Description;
            record.StatusId = model.StatusId;
            record.UserId = model.UserId;
            record.CabinetId = model.CabinetId;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Delete(int id) {
        ViewData["Statuses"] = new SelectList(await _context.Statuses
           .ToListAsync(),
           nameof(Status.StatusId),
           nameof(Status.Description));
        ViewData["Users"] = new SelectList(await _context.Users
            .ToListAsync(),
            nameof(UserModel.UserId),
            nameof(UserModel.Name));
        ViewData["Cabinets"] = new SelectList(await _context.Cabinets
            .ToListAsync(),
            nameof(Cabinet.CabinetId),
            nameof(Cabinet.Number));
        var model = await _context.Tickets
            .Include(c => c.Status)
            .Include(c => c.User)
            .Include(c => c.Cabinet)
            .Where(x => x.TicketId == id)
            .FirstOrDefaultAsync();
        if (model is not null) {
            return View(model);
        }
        return NotFound();
    }

    [HttpPost]
    [Route("[action]/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, Ticket model) {
        if (model is not null) {
            var record = await _context.Tickets
                .Include(c => c.Status)
                .Include(c => c.User)
                .Include(c => c.Cabinet)
                .Where(x => x.TicketId == id)
                .FirstOrDefaultAsync();
            if (record is null) {
                return NotFound();
            }

            _context.Tickets.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}
