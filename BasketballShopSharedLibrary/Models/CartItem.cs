using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballShopSharedLibrary.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string? Size { get; set; }
        public double? ShoeSize { get; set; } 
    }

}
