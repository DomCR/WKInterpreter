using System;
using System.Collections.Generic;
using System.Text;

namespace WKInterpreter
{
    public class MultiLineString : GeometryCollection<LineString>
    {
        public List<LineString> Lines { get { return m_geometries; } }
        /// <summary>
        /// Geometry type of the object, MULTILINESTRING.
        /// </summary>
        public override GeometryType GeometryType { get { return GeometryType.MULTILINESTRING; } }
    }
}
