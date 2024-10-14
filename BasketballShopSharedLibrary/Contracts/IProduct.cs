using BasketballShopSharedLibrary.Models;
using BasketballShopSharedLibrary.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballShopSharedLibrary.Contracts
{
    public interface IProduct
    {
        Task<ServiceResponse> AddProduct(Product model);

        Task<List<Product>> GetAllProducts();

        Task<Product> GetProductById(int id);
        Task UpdateProductQuantitiesAsync(List<Order> orders);
    }
}
