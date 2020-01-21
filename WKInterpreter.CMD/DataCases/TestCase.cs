using System;
using System.Collections.Generic;
using System.Text;

namespace WKInterpreter.CMD
{
    public class TestCase
    {
        GeometryType Type { get; set; }
        DimensionType Dimension { get; set; }
        public string WKT { get; set; }
        public byte[] WKB { get; set; }

    }
}
