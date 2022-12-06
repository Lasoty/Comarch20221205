using Restauracja.Services.ProductApi.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restauracja.Services.ShoppingCartApi.Model;

public class Product : BaseEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public override long Id { get; set; }

    [Required]
    public string Name { get; set; }
    [Range(1, 1000)]
    public double Price { get; set; }
    public string? Description { get; set; }
    public string? CategoryName { get; set; }
    public string? ImageUrl { get; set; }
}
