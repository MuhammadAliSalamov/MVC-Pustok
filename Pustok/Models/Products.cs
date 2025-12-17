namespace Pustok.Models;

public class Product
{
    public int Id { get; set; }
    public string? Brand { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; } 
    public decimal Price { get; set; }
    public decimal OldPrice { get; set; }
    public int Discount { get; set; }
}
