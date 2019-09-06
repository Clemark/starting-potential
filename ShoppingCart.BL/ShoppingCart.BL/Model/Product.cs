using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.BL
{
    public class Product
    {
        public Product()
        {
                
        }

        public int Id { get; private set; }
        public Product(int productId)
        {
            Id = productId;
        }

        public string Name { get; set; }

        public decimal Price { get; set; }

    }
}
