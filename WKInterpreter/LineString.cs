using System;
using System.Collections.Generic;
using System.Text;
using WKInterpreter.Exceptions;

namespace WKInterpreter
{
    public class LineString : Geometry
    {
        /// <summary>
        /// Points forming the line.
        /// </summary>
        public List<Point> Points { get; private set; }
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
        //*********************************************************************************
        public void AddPoint(Point point)
        {
            if (point.Dimension != Dimension)
                throw new InvalidDimensionException();
            if (point.IsEmpty)
                throw new ArgumentException("Cannot add an empty point.");

            Points.Add(point);
        }
    }
}
