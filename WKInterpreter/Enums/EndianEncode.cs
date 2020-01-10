using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WKInterpreter.Emuns
{
    /// <summary>
    /// Defines which endian type is encoded
    /// </summary>
    public enum EndianEncode : byte
    {
        BIG_ENDIAN    = 0x00,
        LITTLE_ENDIAN = 0x01
    }
}
