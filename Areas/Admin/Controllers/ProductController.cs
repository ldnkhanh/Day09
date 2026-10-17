using day09.Data;
using day09.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace day09.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : CrudController<Product>
{
    public ProductController(AppDbContext db) : base(db) { }

    protected override IQueryable<Product> Query() => Db.Products.Include(p => p.Category);

    protected override async Task PrepareAsync()
        => ViewBag.Categories = new SelectList(await Db.Categories.OrderBy(c => c.Name).ToListAsync(), "Id", "Name");

    protected override async Task ValidateAsync(Product m)
    {
        await base.ValidateAsync(m);
        if (m.SalePrice > m.Price)
            ModelState.AddModelError("SalePrice", "Giá khuyến mãi không được lớn hơn giá gốc");
    }
}
