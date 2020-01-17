using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WKInterpreter_initial
{
    /// <summary>
    /// The element can be serialized to WKB or WKT
    /// </summary>
    public interface IWKSerializable
    {
        /// <summary>
        /// Serialize the element to Well Known Text
        /// </summary>
        /// <returns></returns>
        string ToWKT();
        /// <summary>
        /// Serialize the element to Well Known Binary
        /// </summary>
        /// <returns></returns>
        byte[] ToWKB();
    }
}
