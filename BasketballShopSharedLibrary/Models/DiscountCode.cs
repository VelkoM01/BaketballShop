using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballShopSharedLibrary.Models
{
    public class DiscountCode
    {
        public string Code { get; set; } = default!;
        public int DiscountPercentage { get; set; }
    }
}
