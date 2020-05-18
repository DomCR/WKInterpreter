using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WKInterpreter
{
    /// <summary>
    /// MultiLineString class, implements a geometry collection of MultiLineStrings.
    /// </summary>
    public class MultiLineString : GeometryCollection<LineString>, IEquatable<MultiLineString>
    {
        /// <summary>
        /// Lines in the multi line string.
        /// </summary>
        public List<LineString> Lines { get { return m_geometries; } }
        /// <summary>
        /// Geometry type of the object, MULTILINESTRING.
        /// </summary>
        public override GeometryType GeometryType { get { return GeometryType.MULTILINESTRING; } }
        //*********************************************************************************
        public MultiLineString() : base() { }
        public MultiLineString(IEnumerable<LineString> geometries) : base(geometries) { }
        //*********************************************************************************
        /// <summary>
        /// Equality between this object and another
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            if (!(other is MultiLineString))
                return false;

            return Equals((MultiLineString)other);
        }
        /// <summary>
        /// Equality between this object and another
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(MultiLineString other)
        {
            return Lines.SequenceEqual(other.Lines);
        }
        /// <summary>
        /// HashCode.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return new { m_geometries }.GetHashCode();
        }
    }
}
