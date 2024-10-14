using BasketballShopSharedLibrary.Models;

namespace BasketballShopServer.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrdersAsync(List<Order> orders);
    }
}
