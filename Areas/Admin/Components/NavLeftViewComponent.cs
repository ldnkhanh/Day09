using Microsoft.AspNetCore.Mvc;

namespace day09.Areas.Admin.Components;

public class NavLeftViewComponent : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}
