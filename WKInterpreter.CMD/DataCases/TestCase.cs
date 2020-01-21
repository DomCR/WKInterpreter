using System;
using System.Collections.Generic;
using System.Text;

namespace WKInterpreter.CMD
{
    public class TestCase
    {
        GeometryType Type { get; set; }
        DimensionType Dimension { get; set; }
        public string wkt { get; set; }
        public byte[] wkb { get; set; }
        public string ewkt { get; set; }
        public byte[] ewkb { get; set; }
    }
}
