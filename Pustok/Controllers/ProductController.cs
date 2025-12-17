using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Pustok.Controllers;

public class ProductController : Controller
{
    public IActionResult Details()
    {
        return View();
    }
}
