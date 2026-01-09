using System.ComponentModel.DataAnnotations;
using Pustok.Areas.Admin.Models.Base;

namespace Pustok.Areas.Admin.Models;

public class Product : BaseEntity  
{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(30, ErrorMessage = "Name length must be between 3 and 30 characters"), MinLength(3, ErrorMessage = "Name length must be between 3 and 30 characters")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Description is required")]
    [MaxLength(500, ErrorMessage = "Description length must not exceed 500 characters")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "Price is required")]
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }
}
