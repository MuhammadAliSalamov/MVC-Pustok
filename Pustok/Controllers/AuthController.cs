using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace Pustok.Controllers;

public class AuthController : Controller
{
    public IActionResult Auth()
    {
        return View();
    }
}
