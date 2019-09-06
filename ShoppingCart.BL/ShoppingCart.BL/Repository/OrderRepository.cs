using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.BL
{
    internal class OrderRepository
    {
        public static List<Order> orderLists = new List<Order>();

        public static Order Retrieve(Product item)
        {
            return orderLists.FirstOrDefault(e => e.ProductName == item.Name);
        }
        public static IEnumerable<Order> RetrieveAll()
        {
            return orderLists;
        }

        public static bool Add(Product product, int quantity)
        {

            Order item = new Order()
            {
                ProductId = product.Id,
                ProductName = product.Name,
                PurchasePrice = product.Price,
                Quantity = quantity
            };

            orderLists.Add(item);

            return true;
        }

        public static bool Update(Product product, int quantity, bool isAdd)
        {
            Order item = Retrieve(product);

            orderLists[orderLists.IndexOf(item)].Quantity = isAdd 
                ? orderLists[orderLists.IndexOf(item)].Quantity + quantity 
                : orderLists[orderLists.IndexOf(item)].Quantity - quantity;

            return true;
        }

        public static bool Delete(Order item)
        {
            orderLists.Remove(item);

            return true;
        }

        public static bool ClearAll()
        {
            orderLists.Clear();

            return true;
        }
    }
}
