using BasketballShopServer.Data;
using BasketballShopSharedLibrary.Models;

namespace BasketballShopServer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddOrdersAsync(List<Order> orders)
        {
            await _context.Orders.AddRangeAsync(orders);
            await _context.SaveChangesAsync();
        }
    }
}
