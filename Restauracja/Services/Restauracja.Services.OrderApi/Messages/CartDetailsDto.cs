using Restauracja.Common.Model.Dto;
using Restauracja.Services.ShoppingCartApi.Model.Dto;

namespace Restauracja.Services.OrderApi.Messages;

public class CartDetailsDto : BaseDtoEntity
{
    public long? CartHeaderId { get; set; }
    public int ProductId { get; set; }
    public virtual ProductDto Product { get; set; }
    public int Count { get; set; }
}
