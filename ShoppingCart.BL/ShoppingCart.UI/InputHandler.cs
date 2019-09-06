using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.UI
{
    public static class InputHandler
    {
        public static string OptionHandler(bool isOption)
        {
            string _val = String.Empty;
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
                if ((key.KeyChar == 'x' | key.KeyChar == 'c') & String.IsNullOrEmpty(_val) & isOption)
                {
                    _val += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                else if (key.Key != ConsoleKey.Backspace & !(_val.StartsWith("x") | _val.StartsWith("c")))
                {
                    double val = 0;
                    bool _x = double.TryParse(key.KeyChar.ToString(), out val);
                    if (_x)
                    {
                        _val += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && _val.Length > 0)
                    {
                        _val = _val.Substring(0, (_val.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);

            if (String.IsNullOrEmpty(_val))
            {
                if (isOption) return "";
                else return "0";
            }
            else
            {
                return _val;
            }
        }
    }
}
