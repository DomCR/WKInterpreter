using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WKInterpreter
{
    public class MultiLineString : GeometryCollection<LineString>, IEquatable<MultiLineString>
    {
        public List<LineString> Lines { get { return m_geometries; } }
        /// <summary>
        /// Geometry type of the object, MULTILINESTRING.
        /// </summary>
        public override GeometryType GeometryType { get { return GeometryType.MULTILINESTRING; } }
        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            if (!(other is MultiLineString))
                return false;

            return Equals((MultiLineString)other);
        }
        public bool Equals(MultiLineString other)
        {
            return Lines.SequenceEqual(other.Lines);
        }
        public override int GetHashCode()
        {
            return new { m_geometries }.GetHashCode();
        }
    }
}
