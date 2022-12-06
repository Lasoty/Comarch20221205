using Restauracja.Common.Model.Dto;

namespace Restauracja.Services.ShoppingCartApi.Model.Dto;

public class CartDetailsDto : BaseDtoEntity
{
    public long? CartHeaderId { get; set; }
    public virtual CartHeaderDto? CartHeader { get; set; }
    public int ProductId { get; set; }
    public virtual ProductDto Product { get; set; }
    public int Count { get; set; }
}
