using System;
using System.Collections.Generic;
using System.Text;

namespace WKInterpreter
{
    /// <summary>
    /// Point class
    /// </summary>
    public class Point : Geometry
    {
        /// <summary>
        /// X component.
        /// </summary>
        public double? X { set; get; }
        /// <summary>
        /// Y component.
        /// </summary>
        public double? Y { set; get; }
        /// <summary>
        /// Z component.
        /// </summary>
        public double? Z { set; get; }
        /// <summary>
        /// M component.
        /// </summary>
        public double? M { set; get; }
        public override GeometryType GeometryType { get { return GeometryType.POINT; } }

        /// <summary>
        /// Default constructor, creates a 2D point at 0,0.
        /// </summary>
        public Point() : base()
        {
            Dimension = DimensionType.EMPTY; 
        }
    }
}
