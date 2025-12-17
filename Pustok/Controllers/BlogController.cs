using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Pustok.Controllers;

public class BlogController : Controller
{
    public IActionResult Details()
    {
        return View();
    }
}
