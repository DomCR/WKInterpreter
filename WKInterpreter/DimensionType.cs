using System;
using System.Collections.Generic;
using System.Text;

namespace WKInterpreter
{
    /// <summary>
    /// Defines the point dimensions.
    /// </summary>
    public enum DimensionType
    {
        EMPTY = -0x0001,
        XY    = 0x0000,
        XYZ   = 0x03E8,
        XYM   = 0x07D0,
        XYZM  = 0x0BB8
    }
}
