using Microsoft.AspNetCore.Mvc;

namespace Pustok.Areas.Admin.Controllers;

public class HomeController : Controller
{
    [Area("Admin")]
    public IActionResult Index()
    {
        return View();
    }
}
