using day09.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace day09.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;
    public HomeController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        ViewBag.Banners = await _db.Banners.Where(b => b.Status == 1)
            .OrderByDescending(b => b.Priority).ToListAsync();
        var products = await _db.Products.Include(p => p.Category)
            .Where(p => p.Status == 1).OrderByDescending(p => p.Id).Take(6).ToListAsync();
        return View(products);
    }

    public IActionResult About() => View();
    public IActionResult Contact() => View();
}
