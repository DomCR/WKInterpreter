using System;
using System.Collections.Generic;
using System.Text;

namespace WKInterpreter
{
    public class MultiPoint : GeometryCollection<Point>
    {
        public List<Point> Points { get { return m_geometries; } }
        /// <summary>
        /// Geometry type of the object, LINESTRING.
        /// </summary>
        public override GeometryType GeometryType { get { return GeometryType.MULTIPOINT; } }

    }
}
