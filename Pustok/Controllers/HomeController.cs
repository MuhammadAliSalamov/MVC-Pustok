using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Pustok.Models;
using Pustok.ViewModels;

namespace Pustok.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        
        Product product1 = new Product 
            { 
                Id = 1, 
                Brand = "Apple", 
                Name = "iPad with Retina Display", 
                ImageUrl = "image/products/product-1.jpg", 
                Price = 51.20m, 
                OldPrice = 64.00m, 
                Discount = 20 
            };
        Product product2 = new Product 
            { 
                Id = 2, 
                Brand = "Samsung", 
                Name = "Galaxy Tab S7", 
                ImageUrl = "image/products/product-2.jpg", 
                Price = 45.00m, 
                OldPrice = 50.00m, 
                Discount = 10 
            };
        HomeVM homeVM = new HomeVM
        {
            Products = new List<Product> { product1, product2 }
        };
        return View(homeVM);
    }
}
