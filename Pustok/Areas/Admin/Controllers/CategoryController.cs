using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Pustok.Areas.Admin.Models;
using Pustok.DAL;
using Pustok.Utilities.Extensions;
using Pustok.Areas.Admin.ViewModels.Category;
using Pustok.Utilities.Enums;

namespace Pustok.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    public CategoryController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task<IActionResult> Index()
    {
        var dbCategories = await _context.Categories.ToListAsync();
        return View(dbCategories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCategoryVM createCategory)
    {
        if (!ModelState.IsValid) return View(createCategory);

        bool isExist = await _context.Categories.AnyAsync(c => c.Name.ToLower() == createCategory.Name.ToLower());
        if (isExist)
        {
            ModelState.AddModelError("Name", "This category already exists");
            return View(createCategory);
        }
        Category category = new Category
        {
            Name = createCategory.Name,
            Description = createCategory.Description,
        };

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        UpdateCategoryVM updateCategory = new UpdateCategoryVM
        {
            Name = category.Name,
            Description = category.Description,
        };
        return View(updateCategory);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateCategoryVM updateCategory)
    {
        if (id != updateCategory.Id) return BadRequest();
        if (!ModelState.IsValid) return View(updateCategory);

        // bool isExist = await _context.Categories.AnyAsync(c => c.Name.ToLower() == updateCategory.Name.ToLower() && c.Id != id);
        // if (isExist)
        // {
        //     ModelState.AddModelError("Name", "This category already exists");
        //     return View(updateCategory);
        // }
        Category category = await _context.Categories.FindAsync(id);
        if (category == null) 
        {
            return NotFound();
        }
        category.Name = updateCategory.Name;
        category.Description = updateCategory.Description;
        _context.Update(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        Category category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Details(int id)
    {
        var category = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
        if (category == null) return NotFound();
        
        return View(category);
    }
}