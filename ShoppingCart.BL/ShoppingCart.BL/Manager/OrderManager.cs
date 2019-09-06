using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.BL
{
    public static class OrderManager
    {
        public static Product ProductRetrieve(int productId)
        {
            return ProductRepository.Retrieve(productId);
        }
        public static IEnumerable<Product> ProductRetrieveAll()
        {
            return ProductRepository.RetrieveAll();
        }

        public static Order OrderRetrieve(Product item)
        {
            return OrderRepository.Retrieve(item);
        }

        public static IEnumerable<Order> OrderRetrieveAll()
        {
            return OrderRepository.RetrieveAll();
        }

        public static bool Save(int itemId, int quantity)
        {
            Product product = ProductRetrieve(itemId);

            bool success = IsNew(product)
                ? OrderRepository.Add(product, quantity)
                : OrderRepository.Update(product, quantity, true);

            return success;
        }

        public static bool Deduct(int itemId, int quantity)
        {
            bool success = false;

            Product product = ProductRetrieve(itemId);
            Order items = OrderRetrieve(product);


            if (items.Quantity >= quantity)
            {
                if (items.Quantity > quantity)
                {
                    OrderRepository.Update(product, quantity, false);
                    success = true;
                }
                else
                {
                    OrderRepository.Delete(items);
                    success = true;
                }
            }

            return success;
        }

        internal static bool IsNew(Product item)
        {
            bool isNew = true;

            if (OrderRepository.RetrieveAll().Contains(OrderRepository.Retrieve(item))) isNew = false;

            return isNew;
        }

        public static decimal Total()
        {
            decimal _total = 0;

            foreach (Order i in OrderRetrieveAll())
            {
                _total += i.Quantity * i.PurchasePrice;
            }

            return _total;
        }

        public static bool CheckOut()
        {
            return OrderRepository.ClearAll();
        }
    }
}
