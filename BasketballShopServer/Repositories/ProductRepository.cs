using BasketballShopServer.Data;
using BasketballShopSharedLibrary.Contracts;
using BasketballShopSharedLibrary.Models;
using BasketballShopSharedLibrary.Response;
using Microsoft.EntityFrameworkCore;

namespace BasketballShopServer.Repositories
{
    public class ProductRepository : IProduct
    {
        private readonly AppDbContext appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<ServiceResponse> AddProduct(Product model)
        {
            if(model is null) return new ServiceResponse(false, "Model is null");
            var (flag, message) = await CheckName(model.Name!);

            if (flag)
            {
                appDbContext.Products.Add(model);
                await Commit();
                return new ServiceResponse(true, "Product Saved");
            }
            return new ServiceResponse(flag, message);

        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await appDbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await appDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} was not found.");
            }

            return product;
        }

        private async Task<ServiceResponse> CheckName(string name)
        {
            var product = await appDbContext.Products.FirstOrDefaultAsync(x => x.Name.ToLower()!.Equals(name.ToLower()));
            return product is null ? new ServiceResponse(true, null!) : new ServiceResponse(false, "Product already exists"); 
        }

        public async Task UpdateProductQuantitiesAsync(List<Order> orders)
        {
            foreach (var order in orders)
            {
                var product = await appDbContext.Products
                    .FirstOrDefaultAsync(p => p.Id == order.Id);

                if (product != null)
                {
                    product.StockQuantity -= order.Quantity;

                    if (product.StockQuantity < 0)
                    {
                        product.StockQuantity = 0;
                    }

                    appDbContext.Products.Update(product);
                }
            }

            await Commit();
        }

        private async Task Commit() => await appDbContext.SaveChangesAsync();
    }
}
