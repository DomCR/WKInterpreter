using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WKInterpreter
{
    public class Surface : GeometryCollection<Point>
    {
        public List<Point> Shape
        {
            get
            {
                List<Point> m_shape = new List<Point>(m_geometries);
                m_shape.Add(ClosingPoint);

                return m_shape;
            }
        }
        public Point ClosingPoint { get { return m_geometries.FirstOrDefault(); } }
        public override GeometryType GeometryType { get { return GeometryType.SURFACE; } }
        public Surface() : base()
        {

        }
        /// <summary>
        /// Surface validation.
        /// </summary>
        public override bool IsValid
        {
            get
            {
                if (!base.IsValid)
                    return false;

                //More than 3 points
                if (m_geometries.Count < 3)
                    return false;

                return true;
            }
        }
    }
}
