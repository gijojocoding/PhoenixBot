using System;
using System.Collections.Generic;
using System.Text;

namespace PhoenixBot
{
    internal static class Converter
    {
        public static bool ConvertToBool(int number)
        {
            if(number == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static int ConvertFromBool(bool b)
        {
            if(b == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public static ulong ConvertToUlong(string s)
        {
            ulong output = Convert.ToUInt64(s);
            return output;
        }
        public static string ConvertToString(ulong id)
        {
            string output = id.ToString();
            return output;
        }
    }
}
