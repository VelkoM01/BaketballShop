using BasketballShopSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballShopSharedLibrary.Contracts
{
    public interface ICartService
    {
        event Action OnCartChanged;
        List<CartItem> CartItems { get; }
        void AddToCart(Product product, int quantity = 1, string? size = null, double? shoeSize = 0);
        void RemoveFromCart(Product product);
        decimal GetTotalPrice();
        bool IsInCart(Product product);
        int GetCartItemCount();
        void UpdateQuantity(Product product, int quantity);
        Task<string> Checkout(List<Order> cartItems);
        List<CartItem> GetAllCartItems();
    }
}
