using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WKInterpreter
{
    public class Polygon : GeometryCollection<Point>
    {
        public List<Point> ExteriorShape { get { return m_geometries; } }
        public List<Polygon> InteriorShapes { get; private set; }
        public bool IsExterior { get; }
        public Polygon Parent { get; }
        public override GeometryType GeometryType { get { return GeometryType.POLYGON; } }
        public override bool IsValid
        {
            get
            {
                //Different dimension element validation
                if (m_geometries.Select(o => o.Dimension).Distinct().Count() > 1)
                    return false;

                //Check the validation of all the elements
                foreach (Geometry item in m_geometries)
                {
                    if (!item.IsValid)
                        return false;
                }

                //More than 3 points
                if (m_geometries.Count < 3)
                    return false;

                //The polygon must be closed
                if (!m_geometries.FirstOrDefault().IsNear(m_geometries.Last()))
                    return false;

                return true;
            }
        }
    }
}
