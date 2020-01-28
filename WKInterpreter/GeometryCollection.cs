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
    public abstract class GeometryCollection<T> : Geometry where T : Geometry
    {
        /// <summary>
        /// Geometry type of the object, GEOMETRYCOLLECTION. 
        /// </summary>
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
        /// <summary>
        /// List of geometric objects inside the collection.
        /// </summary>
        protected List<T> m_geometries { get; private set; }
        /// <summary>
        /// Default constructor for an empty collection.
        /// </summary>
        public GeometryCollection()
        {
            m_geometries = new List<T>();
        }
        /// <summary>
        /// Initialize a collection.
        /// </summary>
        /// <param name="geometries">Enumerable with the geometries to initialize the object.</param>
        public GeometryCollection(IEnumerable<T> geometries)
        {
            m_geometries = new List<T>(geometries);
        }
        //*********************************************************************************
        /// <summary>
        /// Add a none empty geometry into the collection.
        /// </summary>
        /// <param name="element"></param>
        public virtual void AddGeometry(T element)
        {
            if (!this.IsEmpty && element.Dimension != Dimension)
                throw new InvalidDimensionException();
            if (element.IsEmpty)
                throw new ArgumentException("Cannot add an empty element.");

            m_geometries.Add(element);
        }
    }
    /// <summary>
    /// Collection of generic geometries.
    /// </summary>
    public class GeometryCollection : GeometryCollection<Geometry>
    {
        public List<Geometry> Geometries { get { return m_geometries; } }
    }
}
