using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballShopSharedLibrary.Models
{
    public class CheckoutRequest
    {
        public List<Order> CartItems { get; set; } = new();
        public string DiscountCode { get; set; } = string.Empty;
    }
}
