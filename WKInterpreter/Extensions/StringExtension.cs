using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WKInterpreter.Extensions
{
    public static class StringExtension
    {
        internal static int[] IndexOfAll(this string str, char value)
        {
            List<int> positions = new List<int>();

            for(int i = 0; i < str.Length; i++)
            {
                if (str[i] == value)
                    positions.Add(i);
            }

            return positions.ToArray();
        }
        /// <summary>
        /// Gets a string and returns an array of bytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this string value)
        {
            return Enumerable.Range(0, value.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(value.Substring(x, 2), 16))
                .ToArray();
        }
    }
}
