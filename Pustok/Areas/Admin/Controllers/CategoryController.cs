using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Pustok.Areas.Admin.Models;
namespace Pustok.Areas.Admin.Controllers;

public class CategoryController : Controller
{
    [Area("Admin")]
    public IActionResult Index()
    {
        var categories = new List<Models.Category>
        {
            new Models.Category { Id = 1, Name = "Fiction", Description = "Fictional books" },
            new Models.Category { Id = 2, Name = "Science", Description = "Scientific literature" },
            new Models.Category { Id = 3, Name = "History", Description = "Historical books" }
        };
        return View(categories);
    }
}
