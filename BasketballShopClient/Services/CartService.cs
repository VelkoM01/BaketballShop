using BasketballShopSharedLibrary.Contracts;
using BasketballShopSharedLibrary.Models;

namespace BasketballShopClient.Services
{
    public class CartService(HttpClient httpClient) : ICartService
    {
        public event Action? OnCartChanged;
        private readonly List<CartItem> cartItems = new();

        public List<CartItem> CartItems => cartItems;

        public void AddToCart(Product product, int quantity = 1, string? size = null, double? shoeSize = 0)
        {
            var existingItem = cartItems.FirstOrDefault(item => item.Product.Id == product.Id && item.Size == size && item.ShoeSize == shoeSize);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cartItems.Add(new CartItem { Product = product, Quantity = quantity, Size = size, ShoeSize = shoeSize });
            }

            NotifyCartChanged();
        }

        public void RemoveFromCart(Product product)
        {
            var item = cartItems.FirstOrDefault(i => i.Product.Id == product.Id);
            if (item != null)
            {
                cartItems.Remove(item);
                NotifyCartChanged();
            }
        }

        public decimal GetTotalPrice()
        {
            return cartItems.Sum(item => item.Product.Price * item.Quantity);
        }

        public bool IsInCart(Product product)
        {
            return cartItems.Any(item => item.Product.Id == product.Id);
        }

        public void UpdateQuantity(Product product, int quantity)
        {
            var item = cartItems.FirstOrDefault(i => i.Product.Id == product.Id);
            if (item != null)
            {
                item.Quantity = quantity;
                NotifyCartChanged();
            }
        }

        public int GetCartItemCount()
        {
            return cartItems.Count;
        }

        public List<CartItem> GetAllCartItems()
        {
            return cartItems;
        }

        private void NotifyCartChanged()
        {
            OnCartChanged?.Invoke();
        }

        public async Task<string> Checkout(List<Order> cartItems)
        {
            var response = await httpClient.PostAsync("api/payment/checkout",
            General.GenerateStringContent(General.SerializeObj(cartItems)));

            var url = await response.Content.ReadAsStringAsync();
            return url;
        }
    }
}
