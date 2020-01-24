using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WKInterpreter.Exceptions;

namespace WKInterpreter
{
    /// <summary>
    /// Collection of geometries.
    /// </summary>
    /// <typeparam name="T">Geometry type to define the collection.</typeparam>
    public class GeometryCollection<T> : Geometry where T : Geometry
    {
        public override GeometryType GeometryType { get { return GeometryType.GEOMETRYCOLLECTION; } }
        /// <summary>
        /// Shared dimension for all the elements int the collection.
        /// </summary>
        /// <remarks>
        /// If the element is not valid may be for a different dimension in an element.
        /// </remarks>
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
        /// <summary>
        /// The collection is empty.
        /// </summary>
        public override bool IsEmpty { get { return !m_geometries.Any(); } }
        /// <summary>
        /// GeometryCollection base validation.
        /// </summary>
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
        public virtual void AddGeometry(T element)
        {
            if (!this.IsEmpty && element.Dimension != Dimension)
                throw new InvalidDimensionException();
            if (element.IsEmpty)
                throw new ArgumentException("Cannot add an empty element.");

            m_geometries.Add(element);
        }
    }
}
