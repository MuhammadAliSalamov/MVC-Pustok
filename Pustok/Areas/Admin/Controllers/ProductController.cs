using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Areas.Admin.Models;
using Pustok.Areas.Admin.ViewModels.Product;
using Pustok.DAL;
using Pustok.Utilities.Enums;
using Pustok.Utilities.Extensions;

namespace Pustok.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public ProductController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task<IActionResult> Index()
    {

        var dbProducts = await _context.Products.Include(p => p.Category).ToListAsync();
        return View(dbProducts);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProductVM createProduct)
    {
        if (!ModelState.IsValid) return View(createProduct);

        bool isExist = await _context.Products.AnyAsync(p => p.Name.ToLower() == createProduct.Name.ToLower());
        if (isExist)
        {
            ModelState.AddModelError("Name", "This product already exists");
            return View(createProduct);
        }

        if (!createProduct.ImageFile.ValidateType("image/"))
        {
            ModelState.AddModelError("ImageFile", "Please select image file type");
            return View(createProduct);
        }

        if (createProduct.ImageFile.ValidateSize(FileSize.KB, 2048))
        {
            ModelState.AddModelError("ImageFile", "Image size must be max 2MB");
            return View(createProduct);
        }

        Product product = new Product
        {
            Name = createProduct.Name,
            Description = createProduct.Description,
            Price = createProduct.Price,
            CategoryId = createProduct.CategoryId,
            ImageUrl = await createProduct.ImageFile.CreateFile(_env.WebRootPath, "image", "products")
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        ViewBag.Categories = await _context.Categories.ToListAsync();

        UpdateProductVM updateProduct = new UpdateProductVM
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
            ImageUrl = product.ImageUrl
        };
        return View(updateProduct);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateProductVM updateProduct)
    {
        if (id != updateProduct.Id) return BadRequest();
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(updateProduct);
        }

        Product product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();
        if (updateProduct.ImageFile != null)
        {
            if (!updateProduct.ImageFile.ValidateType("image/"))
            {
                ModelState.AddModelError("ImageFile", "Please select image file type");
                return View(updateProduct);
            }
            if (updateProduct.ImageFile.ValidateSize(FileSize.KB, 2048))
            {
                ModelState.AddModelError("ImageFile", "Image size must be max 2MB");
                return View(updateProduct);
            }

            product.ImageUrl.DeleteFile(_env.WebRootPath, "image", "products");
            product.ImageUrl = await updateProduct.ImageFile.CreateFile(_env.WebRootPath, "image", "products");
        }

        product.Name = updateProduct.Name;
        product.Description = updateProduct.Description;
        product.Price = updateProduct.Price;
        product.CategoryId = updateProduct.CategoryId; // Обновляем категорию

        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        Product product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        if (!string.IsNullOrEmpty(product.ImageUrl))
        {
            product.ImageUrl.DeleteFile(_env.WebRootPath, "image", "products");
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return NotFound();

        return View(product);
    }
}