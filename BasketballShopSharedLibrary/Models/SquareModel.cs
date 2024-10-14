using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballShopSharedLibrary.Models
{
    public class SquareModel
    {
        private bool isShown;
        public int Id { get; set; }
        public string Style { get; set; }
        public bool IsShown
        {
            get => isShown;
            set {
                isShown = value;
                if (isShown)
                {
                    Style = "mole";
                }
                else
                {
                    Style = "";
                }
            }
        }
    }
}
