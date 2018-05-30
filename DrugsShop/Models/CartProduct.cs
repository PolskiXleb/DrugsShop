using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugsShop.Models
{
    public class CartProduct
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public bool ByRecipe { get; set; }

        public int Amount { get; set; }

        public int Sum { get; set; }
    }
}