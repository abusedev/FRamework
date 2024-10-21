using System;

namespace FRamework.handlers.types
{
    public class converter
    {
        public static dynamic getType(object input)
        {
            return Convert.GetTypeCode(input);
        }

        public static dynamic changeType(string input, Type type)
        {
            return Convert.ChangeType(input, type);
        }

        public static bool toBool(dynamic input)
        {
            return Convert.ToBoolean(input);
        }
        
        public static byte toByte(dynamic input)
        {
            return Convert.ToByte(input);
        }

        public static char toChar(dynamic input)
        {
            return Convert.ToChar(input);
        }

        public static decimal toDecimal(dynamic input)
        {
            return Convert.ToDecimal(input);
        }

        public static double toDouble(dynamic input)
        {
            return Convert.ToDouble(input);
        }

        public static int toInt(dynamic input)
        {
            return Convert.ToInt32(input);
        }

        public static string toString(dynamic input)
        {
            return Convert.ToString(input);
        }
    }
}
