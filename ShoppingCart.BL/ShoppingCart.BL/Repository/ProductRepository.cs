using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.BL
{
    internal class ProductRepository
    {
        public static Product Retrieve(int productId)
        {
            return RetrieveAll().FirstOrDefault(e => e.Id == productId);
        }

        public static IEnumerable<Product> RetrieveAll()
        {
            return File.ReadAllLines("Products.csv")
                .ToList()
                .Select(x => new Product(Convert.ToInt32(x.Split(',')[0]))
                {
                    Name = x.Split(',')[1],
                    Price = Convert.ToDecimal(x.Split(',')[2]),
                });
        }

    }
}
