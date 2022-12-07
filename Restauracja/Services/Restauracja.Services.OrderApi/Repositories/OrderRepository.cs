using Microsoft.EntityFrameworkCore;
using Restauracja.Services.OrderApi.Data;
using Restauracja.Services.OrderApi.Model;

namespace Restauracja.Services.OrderApi.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

    public OrderRepository(DbContextOptions<ApplicationDbContext> dbContextOptions)
    {
        this._dbContextOptions = dbContextOptions;
    }
    public async Task<bool> AddOrder(OrderHeader orderHeader)
    {
        await using var db = new ApplicationDbContext(_dbContextOptions);
        db.OrderHeaders.Add(orderHeader);
        return await db.SaveChangesAsync() > 0;
    }
}
