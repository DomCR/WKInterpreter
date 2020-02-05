using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WKInterpreter.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Gets a string and returns an array of bytes.
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
        /// <summary>
        /// Reads a string until it finds a character.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="c">Character to find.</param>
        /// <returns></returns>
        public static string ReadUntil(this string line, char c)
        {
            string value = "";

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == c)
                    break;
                else
                    value += line[i];
            }

            return value;
        }
    }
}
