using day09.Data;
using Microsoft.AspNetCore.Mvc;

namespace day09.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    private readonly AppDbContext _db;
    public DashboardController(AppDbContext db) => _db = db;

    public IActionResult Index()
    {
        ViewBag.Categories = _db.Categories.Count();
        ViewBag.Products = _db.Products.Count();
        ViewBag.Banners = _db.Banners.Count();
        ViewBag.Blogs = _db.Blogs.Count();
        return View();
    }
}
