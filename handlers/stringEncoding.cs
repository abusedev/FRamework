using System;
using System.Text;

namespace FRamework.handlers
{
    public class stringEncoding
    {
        public static string encode(string input)
        {
            var text = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(text);
        }

        public static string decode(string input) 
        {
            var encoded = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(encoded);
        } 
    }
}