using System;
using System.Collections.Generic;
using System.Text;

namespace WKInterpreter
{
    public class LineString : Geometry
    {
        public List<Point> Points { get; set; }
        /// <summary>
        /// Geometry type of the object, LINESTRING.
        /// </summary>
        public override GeometryType GeometryType { get { return GeometryType.LINESTRING; } }
        public override DimensionType Dimension => throw new NotImplementedException();
        public override bool IsEmpty => throw new NotImplementedException();
        public override bool IsValid => throw new NotImplementedException();

        public LineString()
        {
            Points = new List<Point>();
        }
        public LineString(IEnumerable<Point> points)
        {
            Points = new List<Point>(points);
        }
    }
}
