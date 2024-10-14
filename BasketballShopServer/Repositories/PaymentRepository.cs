using BasketballShopSharedLibrary.Models;
using Stripe;
using Stripe.Checkout;

namespace BasketballShopServer.Repositories
{
    public class PaymentRepository : IPayment
    { 
        public PaymentRepository() {
            StripeConfiguration.ApiKey = "sk_test_51PxcpuRvD15lgKDNpf07UVi6VlENcABNtoOsXVaTVBgW9iTFAxMBYH2cFPXQqmtH2haRSy5s4ivO1Q1Wv0JixRYU0029OqNnQ5";
        }

        private static readonly List<DiscountCode> DiscountCodes = new List<DiscountCode>
        {
            new DiscountCode { Code = "DISCOUNT5", DiscountPercentage = 5 },
            new DiscountCode { Code = "DISCOUNT10", DiscountPercentage = 10 },
            new DiscountCode { Code = "DISCOUNT15", DiscountPercentage = 15 },
            new DiscountCode { Code = "DISCOUNT20", DiscountPercentage = 20 },
            new DiscountCode { Code = "DISCOUNT25", DiscountPercentage = 25 }
        };

        public string CreateCheckoutSession(List<Order> cartItems, string discountCode)
        {
            if (cartItems == null || !cartItems.Any())
                return null!;

            var totalPrice = cartItems.Sum(ci => ci.Price * ci.Quantity);

            var discountPercentage = GetDiscountPercentage(discountCode);
            var discountAmount = (decimal)((100-discountPercentage) / 100.0);

            var lineItems = new List<SessionLineItemOptions>();
            foreach (var item in cartItems)
            {
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = (item.Price * discountAmount) * 100, 
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name,
                            Description = item.Id.ToString()
                        },
                    },
                    Quantity = item.Quantity
                });
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = ["card"],
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://localhost:7001/order-success",
                CancelUrl = "https://localhost:7001",
            };

            var service = new SessionService();
            Session session = service.Create(options);
            return session.Url;
        }

        private int GetDiscountPercentage(string code)
        {
            var discountCode = DiscountCodes.FirstOrDefault(dc => dc.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
            return discountCode?.DiscountPercentage ?? 0;
        }
    }
}
