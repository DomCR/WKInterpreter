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
        public override DimensionType Dimension
        {
            get
            {
                if (Z.HasValue && !M.HasValue)
                {
                    return DimensionType.XYZ;
                }
                if (M.HasValue && !Z.HasValue)
                {
                    return DimensionType.XYM;
                }
                if (M.HasValue && Z.HasValue)
                {
                    return DimensionType.XYZM;
                }

                return DimensionType.XY;
            }
        }
        public override bool IsEmpty { get { return (!X.HasValue || double.IsNaN(X.Value)) && (!Y.HasValue || double.IsNaN(Y.Value)); } }

        //*********************************************************************************
        /// <summary>
        /// Default constructor, creates an empty point.
        /// </summary>
        public Point() : base()
        {
            X = null;
            Y = null;
            Z = null;
            M = null;
        }

    }
}
