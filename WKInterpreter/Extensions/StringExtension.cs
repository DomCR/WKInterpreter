using System;
using System.Collections.Generic;
using System.Text;

namespace WKInterpreter.Extensions
{
    internal static class StringExtension
    {
        public static int[] IndexOfAll(this string str, char value)
        {
            List<int> positions = new List<int>();

            for(int i = 0; i < str.Length; i++)
            {
                if (str[i] == value)
                    positions.Add(i);
            }

            return positions.ToArray();
        }
    }
}
