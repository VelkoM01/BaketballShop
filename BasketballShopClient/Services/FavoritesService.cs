using BasketballShopSharedLibrary.Contracts;
using BasketballShopSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class FavoritesService : IFavoritesService
{
    public event Action OnFavoritesChanged;

    private List<Product> _favorites = new List<Product>();

    public int GetFavoritesCount()
    {
        return _favorites.Count;
    }

    public async Task AddToFavorites(Product product)
    {
        if (!_favorites.Any(p => p.Id == product.Id))
        {
            _favorites.Add(product);
            OnFavoritesChanged?.Invoke();
        }
        await Task.CompletedTask; 
    }

    public async Task RemoveFromFavorites(int productId)
    {
        var productToRemove = _favorites.FirstOrDefault(p => p.Id == productId);
        if (productToRemove != null)
        {
            _favorites.Remove(productToRemove);
            OnFavoritesChanged?.Invoke();
        }
        await Task.CompletedTask; 
    }

    public async Task<bool> IsProductInFavorites(int productId)
    {
        var result = _favorites.Any(p => p.Id == productId);
        return await Task.FromResult(result);
    }

    public async Task<List<Product>> GetFavorites()
    {
        return await Task.FromResult(_favorites);
    }
}
