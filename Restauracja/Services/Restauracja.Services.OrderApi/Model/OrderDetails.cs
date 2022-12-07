using Restauracja.Services.ProductApi.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restauracja.Services.OrderApi.Model;

public class OrderDetails : BaseEntity
{
    public long OrderHeaderId { get; set; }

    [ForeignKey(nameof(OrderHeaderId))]
    public OrderHeader OrderHeader { get; set; }

    public long ProductId { get; set; }

    public int Count { get; set; }

    public string ProductName { get; set; }

    public double Price { get; set; }
}
