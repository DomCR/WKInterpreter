using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WKInterpreter.Exceptions;

namespace WKInterpreter
{
    public class GeometryCollection<T> : Geometry, IEnumerable<T> where T : Geometry
    {
        public override GeometryType GeometryType { get { return GeometryType.GEOMETRYCOLLECTION; } }
        public override DimensionType Dimension
        {
            get
            {
                if (m_geometries.Count > 0)
                    return m_geometries.FirstOrDefault().Dimension;
                else
                    //If there are no points return a default
                    return DimensionType.XY;
            }
        }
        public override bool IsEmpty { get { return !m_geometries.Any(); } }
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

                return true;
            }
        }

        protected List<T> m_geometries { get; private set; }

        public GeometryCollection()
        {
            m_geometries = new List<T>();
        }
        public GeometryCollection(IEnumerable<T> geometries)
        {
            m_geometries = new List<T>(geometries);
        }
        //*********************************************************************************
        public virtual void Add(T element)
        {
            if (element.Dimension != Dimension)
                throw new InvalidDimensionException();
            if (element.IsEmpty)
                throw new ArgumentException("Cannot add an empty point.");

            m_geometries.Add(element);
        }
        //*********************************************************************************
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
