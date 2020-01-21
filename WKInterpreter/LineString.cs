using System;
using System.Collections.Generic;
using System.Linq;
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
        public override DimensionType Dimension
        {
            get
            {
                if (Points.Count > 0)
                    return Points.FirstOrDefault().Dimension;
                else
                    //If there are no points return a default
                    return DimensionType.XY;
            }
        }
        public override bool IsEmpty { get { return !Points.Any(); } }
        public override bool IsValid
        {
            get
            {
                //Different dimension points
                if (Points.Select(o => o.Dimension).Distinct().Count() > 1)
                    return false;

                //Check the validation of all the points
                foreach (Point point in Points)
                {
                    if (!point.IsValid)
                        return false;
                }

                return true;
            }
        }

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
        public void ChangeDimension(DimensionType dimension)
        {
            throw new NotImplementedException();
        }
    }
    public class _LineString : GeometryCollection<Point>
    {
        public List<Point> Points { get { return m_geometries; } }
        /// <summary>
        /// Geometry type of the object, LINESTRING.
        /// </summary>
        public override GeometryType GeometryType { get { return GeometryType.LINESTRING; } }
        /// <summary>
        /// Check if the line is closed.
        /// </summary>
        /// <returns></returns>
        public bool IsClosed()
        {
            if (m_geometries.Count == 0)
                throw new Exception();

            if (m_geometries.Count == 1)
                return false;

            return m_geometries.FirstOrDefault().IsNear(m_geometries.Last());
        }
    }
}
