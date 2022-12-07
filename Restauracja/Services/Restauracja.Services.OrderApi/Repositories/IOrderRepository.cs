using Restauracja.Services.OrderApi.Model;

namespace Restauracja.Services.OrderApi.Repositories;

public interface IOrderRepository
{
    Task<bool> AddOrder(OrderHeader orderHeader);
}
