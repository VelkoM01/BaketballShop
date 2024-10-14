using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballShopSharedLibrary.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        [Required, Range(0.1, 99999.99)]
        public decimal Price { get; set; }
        [Required, DisplayName("Product Image")]
        public string ImageUrl { get; set; }
        public int StockQuantity { get; set; }
        public ProductType ProductType { get; set; } 


        public string? Size { get; set; }  
        public string? Team { get; set; }  
        public string? Player { get; set; }  
        public double? ShoeSize { get; set; } 
    }
}
