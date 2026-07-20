using System.ComponentModel.DataAnnotations;

namespace RetailInventory.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    // Navigation Property (One Category -> Many Products)
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
