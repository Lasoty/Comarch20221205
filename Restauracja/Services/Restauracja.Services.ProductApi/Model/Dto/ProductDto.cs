namespace Restauracja.Services.ProductApi.Model.Dto;

public class ProductDto : BaseDtoEntity
{
    public string Name { get; set; }

    public double Price { get; set; }

    public string? Description { get; set; }

    public string CategoryName { get; set; }

    public string? ImageUrl { get; set; }
}