using System.ComponentModel.DataAnnotations;

namespace Restauracja.Services.ProductApi.Model;

public class Product : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Range(1, 1000)]
    public double Price { get; set; }
    public string? Description { get; set; }
    public string CategoryName { get; set; }

    [MaxLength(2048)]
    public string ImageUrl { get; set; }
}
