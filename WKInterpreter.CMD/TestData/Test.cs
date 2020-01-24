using System;
using System.Collections.Generic;
using System.Text;

namespace WKInterpreter.CMD.TestData
{
    public class Test
    {
        public string Id { get; set; }
        public GeometryType Type { get; set; }
        public DimensionType Dimension { get; set; }
        public string wkt { get; set; }
        public byte[] wkb_big { get; set; }
        public byte[] wkb_little { get; set; }
        public string ewkt { get; set; }
        public byte[] ewkb_big { get; set; }
        public byte[] ewkb_little { get; set; }
        public Geometry Validation { get; set; }

        public override string ToString()
        {
            return wkt;
        }
    }
}