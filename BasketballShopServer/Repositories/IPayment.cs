using BasketballShopSharedLibrary.Models;
using Stripe.Checkout;

namespace BasketballShopServer.Repositories
{
    public interface IPayment
    {
        string CreateCheckoutSession(List<Order> cartItems, string discountCode);
    }
}
