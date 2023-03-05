using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Helpdesk.Models;

namespace Helpdesk.WebApp.Controllers;

[Route("[controller]")]
public class UserController : Controller {
    private readonly ILogger<UserController> _logger;
    private readonly ApplicationDbContext _context;

    public UserController(ILogger<UserController> logger,
        ApplicationDbContext context) {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index() {
        var models = await _context.Users.ToListAsync();
        return View(models);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Details(int id) {
        var model = await _context.Users
            .Where(x => x.UserId == id)
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
    public async Task<IActionResult> Create(User model) {
        if (model is not null) {
            _context.Users.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Edit(int id) {
        var model = await _context.Users
            .Where(x => x.UserId == id)
            .FirstOrDefaultAsync();
        if (model is not null) {
            return View(model);
        }
        return NotFound();
    }

    [HttpPost]
    [Route("[action]/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, User model) {
        if (model is not null) {
            var record = await _context.Users
                .Where(x => x.UserId == id)
                .FirstOrDefaultAsync();
            if (record is null) {
                return NotFound();
            }

            record.Name = model.Name;
            record.Phone = model.Phone;
            record.Position = model.Position;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<IActionResult> Delete(int id) {
        var model = await _context.Users
            .Where(x => x.UserId == id)
            .FirstOrDefaultAsync();
        if (model is not null) {
            return View(model);
        }
        return NotFound();
    }

    [HttpPost]
    [Route("[action]/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, User model) {
        if (model is not null) {
            var record = await _context.Users
                .Where(x => x.UserId == id)
                .FirstOrDefaultAsync();
            if (record is null) {
                return NotFound();
            }

            _context.Users.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}
