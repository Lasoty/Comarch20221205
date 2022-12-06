using Restauracja.Services.ProductApi.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restauracja.Services.ShoppingCartApi.Model;

public class CartDetails : BaseEntity
{
    public long CartHeaderId { get; set; }
    [ForeignKey("CartHeaderId")]
    public virtual CartHeader CartHeader { get; set; }
    public long ProductId { get; set; }
    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }
    public int Count { get; set; }
}
