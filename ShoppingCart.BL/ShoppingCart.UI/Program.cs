using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShoppingCart.BL;
using ShoppingCart.UI;

namespace ShoppingCart
{
    public class Program
    {
        public static string _shop = "\n----------  AVAILABLE ITEMS  ----------\n" +
                              "\n  ID  |      Item Name       |  Price        " +
                              "\n----------------------------------------";
        public static string _cart = "----------------  SHOPPING CART  ---------------------\n" +
                              "\n  ID  |      Item Name       |  Quantity  |  Price      " +
                              "\n------------------------------------------------------";
        public static int _products;

        static void Main(string[] args)
        {
            int _item;
            int _quantity;
            int _delete;
            int _orders = 0;


            bool _retry = true;
            string _option = String.Empty;
            do
            {
                _orders = OrderManager.OrderRetrieveAll().Count();
                _products = OrderManager.ProductRetrieveAll().Count();
                Console.Clear();
                if (_orders > 0) CartDisplay();
                ShopDisplay();

                Console.Write("\n[x]Remove / Delete Quantity Item | [c]Checkout" +
                             $"\nProduct[1-{_products}]: ");
                _option = InputHandler.OptionHandler(true);


                if ((_option == "x" & _orders > 0))
                {
                    do
                    {
                        Console.Clear();
                        CartDisplay();

                        Console.Write("\nOrder ID: ");
                        _delete = Convert.ToInt32(InputHandler.OptionHandler(false));
                    }
                    while (_delete > _orders | _delete <= 0);

                    Console.Write("\nQuantity: ");
                    _quantity = Convert.ToInt32(InputHandler.OptionHandler(false));


                    if (OrderManager.Deduct(_delete, _quantity))
                    {
                        MessageBox.Show("Edit Saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Quantity Exceeded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else if ((_option == "c" & _orders > 0))
                {
                    _retry = Checkout();
                }
                else if (_option.All(Char.IsDigit) & _option != "")
                {
                    _item = Convert.ToInt32(_option);

                    if (_item > 0 & _item <= _products)
                    {
                        Console.Write("\nQuantity: ");
                        _quantity = Convert.ToInt32(InputHandler.OptionHandler(false));

                        if (OrderManager.Save(_item, _quantity))
                        {
                            MessageBox.Show("Product Added", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Unable to Add Product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Product not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (_option == "")
                {
                    MessageBox.Show("Empty Option", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Shopping Cart Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } while (_retry);
        }


        private static bool Checkout()
        {
            Console.Clear();
            CartDisplay();
            decimal _total;
            int _amount;
            bool _retry = true;

            _total = OrderManager.Total();
            Console.Write($"\nTotal Amount:    {_total}");

            Console.Write("\nAmount Tendered: ");
            _amount = Convert.ToInt32(InputHandler.OptionHandler(false));

            if (_amount >= _total)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to checkout?", "Checkout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Console.WriteLine("\n---------------\nChange: {0}", _amount - _total);

                    Console.WriteLine("---------------\n\nThank you for shopping with us!");
                    Console.WriteLine("Press [any key] to transact again or press [ESC] to terminate application.");


                    if (Console.ReadKey().Key == ConsoleKey.Escape) _retry = false;

                    OrderManager.CheckOut();

                }

            }
            else
            {
                MessageBox.Show("Insufficient amount tendered", "Checkout Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _retry;

        }

        private static void CartDisplay()
        {
            Console.WriteLine(_cart);

            foreach (Order items in OrderManager.OrderRetrieveAll())
            {
                Console.WriteLine(String.Format("   {0,-2} |  {1,-18}  |    {2,4}    |   {3,4}", items.ProductId, items.ProductName, items.Quantity, items.PurchasePrice));
            }

        }

        static void ShopDisplay()
        {
            Console.WriteLine(_shop);

            foreach (Product item in OrderManager.ProductRetrieveAll())
            {
                Console.WriteLine(String.Format(" {0,4} |  {1,-18}  |   {2,4}", item.Id, item.Name, item.Price));
            }
        }
    }
}
