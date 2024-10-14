using BasketballShopSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballShopSharedLibrary.Contracts
{
    public interface IFavoritesService
    {
        Task AddToFavorites(Product product);
        Task RemoveFromFavorites(int productId);
        Task<bool> IsProductInFavorites(int productId);
        Task<List<Product>> GetFavorites();
        int GetFavoritesCount();
        event Action OnFavoritesChanged;
    }

}
