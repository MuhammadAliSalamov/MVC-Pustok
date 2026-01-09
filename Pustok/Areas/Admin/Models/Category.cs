using System.ComponentModel.DataAnnotations;
using Pustok.Areas.Admin.Models.Base;

namespace Pustok.Areas.Admin.Models;

public class Category : BaseEntity
{
    [Required (ErrorMessage = "Name is required")]
    [MaxLength(30, ErrorMessage = "Name length must be between 3 and 30 characters"), MinLength(3, ErrorMessage = "Name length must be between 3 and 30 characters")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Description is required")]
    [MaxLength(500, ErrorMessage = "Description length must not exceed 500 characters")]
    public string? Description { get; set; }
    public ICollection<Product>? Products { get; set; }
}
