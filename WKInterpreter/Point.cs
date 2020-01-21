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
        /// <summary>
        /// Geometry type of the object, POINT.
        /// </summary>
        public override GeometryType GeometryType { get { return GeometryType.POINT; } }
        /// <summary>
        /// Dimensions of the object, based on the non null values.
        /// </summary>
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
        /// <summary>
        /// X and Y the components of the object are null or NaN.
        /// </summary>
        public override bool IsEmpty { get { return (!X.HasValue || double.IsNaN(X.Value)) && (!Y.HasValue || double.IsNaN(Y.Value)); } }
        /// <summary>
        /// Return if the object is a valid one based on it's components.
        /// </summary>
        public override bool IsValid
        {
            get
            {
                //Not valid if 1 of the 2 basic components is null and the other is not
                if (!X.HasValue && Y.HasValue || !Y.HasValue && X.HasValue)
                    return false;
                //Not valid if X or Y don't have value and Z or M have
                if ((!X.HasValue || !Y.HasValue) && (Z.HasValue || M.HasValue))
                    return false;

                return true;
            }
        }
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
        public Point(double? x, double? y) : this()
        {
            X = x;
            Y = y;
        }
        public Point(double? x, double? y, double? z) : this(x, y)
        {
            Z = z;
        }
        public Point(double? x, double? y, double? z, double? m) : this(x, y, z)
        {
            M = m;
        }
        //*********************************************************************************
        /// <summary>
        /// Check if the point is near or at the same coordinate as this one.
        /// </summary>
        /// <param name="other"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public bool IsNear(Point other, double tolerance = 0.0d)
        {
            throw new NotImplementedException();
        }
    }
}
