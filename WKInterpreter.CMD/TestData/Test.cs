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
        public string wkb { get; set; }
        public string ewkt { get; set; }
        public string ewkb { get; set; }
        public string wkbxdr { get; set; }
        public string ewkbxdr { get; set; }
        public string ewkbnosrid { get; set; }
        public string ewkbxdrnosrid { get; set; }
        public Geometry Validation { get; set; }

        public override string ToString()
        {
            return wkt;
        }
    }
}