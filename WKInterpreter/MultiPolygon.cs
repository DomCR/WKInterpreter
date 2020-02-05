using System;

namespace WKInterpreter
{
    public class MultiPolygon : GeometryCollection<Polygon>, IEquatable<MultiPolygon>
    {
        public bool Equals(MultiPolygon other)
        {
            throw new NotImplementedException();
        }
    }
}