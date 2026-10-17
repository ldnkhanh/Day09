using System.Globalization;
using day09.Data;
using day09.Models;
using Microsoft.EntityFrameworkCore;

// Dùng dấu "." cho số thực (giá sản phẩm) để khớp với <input type="number">
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// Tạo DB + dữ liệu mẫu lần chạy đầu
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    if (!db.Categories.Any())
    {
        var c1 = new Category { Name = "Điện thoại", Description = "Các dòng điện thoại" };
        var c2 = new Category { Name = "Laptop", Description = "Máy tính xách tay" };
        db.Categories.AddRange(c1, c2);
        db.SaveChanges();
        db.Products.AddRange(
            new Product { Name = "Phone A", Price = 500, SalePrice = 450, CategoryId = c1.Id },
            new Product { Name = "Laptop B", Price = 1200, CategoryId = c2.Id });
        db.Banners.Add(new Banner { Name = "Banner chào mừng", Description = "Chào mừng đến với day09" });
        db.Blogs.Add(new Blog { Name = "Bài viết đầu tiên", Description = "Nội dung mẫu" });
        db.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment()) app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
