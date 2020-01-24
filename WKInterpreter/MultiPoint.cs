using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WKInterpreter
{
    public class MultiPoint : GeometryCollection<Point>, IEquatable<MultiPoint>
    {
        public List<Point> Points { get { return m_geometries; } }
        /// <summary>
        /// Geometry type of the object, LINESTRING.
        /// </summary>
        public override GeometryType GeometryType { get { return GeometryType.MULTIPOINT; } }

        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            if (!(other is MultiPoint))
                return false;

            return Equals((MultiPoint)other);
        }

        public bool Equals(MultiPoint other)
        {
            return Points.SequenceEqual(other.Points);
        }

        public override int GetHashCode()
        {
            return new { Points }.GetHashCode();
        }
    }
}
