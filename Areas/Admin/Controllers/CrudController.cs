using day09.Data;
using day09.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace day09.Areas.Admin.Controllers;

/// <summary>Controller CRUD dùng chung cho Category, Product, Banner, Blog.</summary>
public abstract class CrudController<T> : Controller where T : class, IEntity, new()
{
    protected readonly AppDbContext Db;
    protected CrudController(AppDbContext db) => Db = db;

    protected DbSet<T> Set => Db.Set<T>();
    protected virtual IQueryable<T> Query() => Set;
    protected virtual Task PrepareAsync() => Task.CompletedTask;   // nạp dữ liệu cho form (dropdown...)

    // Validate: Name không trùng
    protected virtual async Task ValidateAsync(T m)
    {
        var name = m.Name?.Trim();
        if (!string.IsNullOrEmpty(name) &&
            await Set.AnyAsync(x => EF.Property<string>(x, "Name") == name && EF.Property<int>(x, "Id") != m.Id))
            ModelState.AddModelError("Name", "Tên đã tồn tại");
    }

    public async Task<IActionResult> Index()
        => View(await Query().OrderByDescending(x => EF.Property<int>(x, "Id")).ToListAsync());

    public async Task<IActionResult> Create()
    {
        await PrepareAsync();
        return View(new T());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(T model)
    {
        await ValidateAsync(model);
        if (!ModelState.IsValid) { await PrepareAsync(); return View(model); }
        Set.Add(model);
        await Db.SaveChangesAsync();
        TempData["msg"] = "Thêm mới thành công";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var m = await Set.FindAsync(id);
        if (m == null) return NotFound();
        await PrepareAsync();
        return View(m);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, T model)
    {
        if (id != model.Id) return NotFound();
        await ValidateAsync(model);
        if (!ModelState.IsValid) { await PrepareAsync(); return View(model); }
        Db.Update(model);
        await Db.SaveChangesAsync();
        TempData["msg"] = "Cập nhật thành công";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var m = await Set.FindAsync(id);
        if (m != null)
        {
            try { Set.Remove(m); await Db.SaveChangesAsync(); TempData["msg"] = "Đã xóa"; }
            catch (DbUpdateException) { TempData["err"] = "Không thể xóa vì còn dữ liệu liên quan"; }
        }
        return RedirectToAction(nameof(Index));
    }
}
