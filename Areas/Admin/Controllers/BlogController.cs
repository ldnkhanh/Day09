using day09.Data;
using day09.Models;
using Microsoft.AspNetCore.Mvc;

namespace day09.Areas.Admin.Controllers;

[Area("Admin")]
public class BlogController : CrudController<Blog>
{
    public BlogController(AppDbContext db) : base(db) { }
}
