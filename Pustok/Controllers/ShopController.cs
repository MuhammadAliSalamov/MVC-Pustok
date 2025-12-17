
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
namespace Pustok.Controllers;

public class ShopController : Controller
{
    public IActionResult Cart()
    {
        return View();
    }
}
