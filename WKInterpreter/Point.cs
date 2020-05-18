using System;
using System.Collections.Generic;
using System.Text;
using WKInterpreter.Extensions;

namespace WKInterpreter
{
    /// <summary>
    /// Point class.
    /// </summary>
    public class Point : Geometry, IEquatable<Point>
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
        /// <summary>
        /// Initialize a 2D point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(double? x, double? y) : this()
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Initialize a 3D point, using the z component.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Point(double? x, double? y, double? z) : this(x, y)
        {
            Z = z;
        }
        /// <summary>
        /// Initialize a 4D point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="m"></param>
        public Point(double? x, double? y, double? z, double? m) : this(x, y, z)
        {
            M = m;
        }
        /// <summary>
        /// Initalize a point using an array and a dimension.
        /// </summary>
        /// <param name="components"></param>
        /// <param name="dimension"></param>
        public Point(double[] components, DimensionType dimension = DimensionType.XY)
        {
            if (components.Length < 2) throw new ArgumentException("Components must have at least 2 values.");
            if (components.Length != dimension.GetDimensionValue()) throw new ArgumentException("Components must be the same size as the dimension value.");

            X = components[0];
            Y = components[1];

            switch (dimension)
            {
                case DimensionType.XY:
                    break;
                case DimensionType.XYZ:
                    Z = components[2];
                    break;
                case DimensionType.XYM:
                    M = components[2];
                    break;
                case DimensionType.XYZM:
                    Z = components[2];
                    M = components[3];
                    break;
                default:
                    break;
            }
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
            //Argument validation
            double dist = DistanceFrom(other);
            if (tolerance < 0)
                throw new ArgumentOutOfRangeException("Tolerance cannot be less than 0.");

            return dist <= tolerance;
        }
        /// <summary>
        /// Check if the point is near or at the same coordinate as this one.
        /// </summary>
        /// <param name="other"></param>
        /// <param name="tolerance"></param>
        /// <param name="dimension"></param>
        /// <returns></returns>
        public bool IsNear(Point other, DimensionType dimension, double tolerance = 0.0d)
        {
            //Argument validation
            double dist = DistanceFrom(other, dimension);
            if (tolerance < 0)
                throw new ArgumentOutOfRangeException("Tolerance cannot be less than 0.");

            return dist <= tolerance;
        }
        /// <summary>
        /// Returns the distance from another point.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public double DistanceFrom(Point other)
        {
            //Argument validation
            if (other.IsEmpty || this.IsEmpty)
                throw new ArgumentException("Point cannot be empty.");
            if (this.Dimension != other.Dimension)
                throw new ArgumentException("Points must have the same dimension.");

            double xdis = Math.Pow(X.GetValueOrDefault() - other.X.GetValueOrDefault(), 2);
            double ydis = Math.Pow(Y.GetValueOrDefault() - other.Y.GetValueOrDefault(), 2);
            double zdis = 0.0d;
            double mdis = 0.0d;

            switch (this.Dimension)
            {
                case DimensionType.XY:
                    break;
                case DimensionType.XYZ:
                    zdis = Math.Pow(Z.GetValueOrDefault() - other.Z.GetValueOrDefault(), 2);
                    break;
                case DimensionType.XYM:
                    mdis = Math.Pow(M.GetValueOrDefault() - other.M.GetValueOrDefault(), 2);
                    break;
                case DimensionType.XYZM:
                    zdis = Math.Pow(Z.GetValueOrDefault() - other.Z.GetValueOrDefault(), 2);
                    mdis = Math.Pow(M.GetValueOrDefault() - other.M.GetValueOrDefault(), 2);
                    break;
                default:
                    break;
            }

            return Math.Sqrt(xdis + ydis + zdis + mdis);
        }
        /// <summary>
        /// Returns the distance from another point in a defined dimension.
        /// </summary>
        /// <param name="other"></param>
        /// <param name="dimension"></param>
        /// <returns></returns>
        public double DistanceFrom(Point other, DimensionType dimension)
        {
            //Argument validation
            if (other.IsEmpty || this.IsEmpty)
                throw new ArgumentException("Point cannot be empty.");
            if (dimension.GetDimensionValue() > other.Dimension.GetDimensionValue())
                throw new ArgumentException("Points must have an equivalent dimension.");

            double xdis = Math.Pow(X.GetValueOrDefault() - other.X.GetValueOrDefault(), 2);
            double ydis = Math.Pow(Y.GetValueOrDefault() - other.Y.GetValueOrDefault(), 2);
            double zdis = 0.0d;
            double mdis = 0.0d;

            switch (dimension)
            {
                case DimensionType.XY:
                    break;
                case DimensionType.XYZ:
                    zdis = Math.Pow(Z.GetValueOrDefault() - other.Z.GetValueOrDefault(), 2);
                    break;
                case DimensionType.XYM:
                    mdis = Math.Pow(M.GetValueOrDefault() - other.M.GetValueOrDefault(), 2);
                    break;
                case DimensionType.XYZM:
                    zdis = Math.Pow(Z.GetValueOrDefault() - other.Z.GetValueOrDefault(), 2);
                    mdis = Math.Pow(M.GetValueOrDefault() - other.M.GetValueOrDefault(), 2);
                    break;
                default:
                    break;
            }

            return Math.Sqrt(xdis + ydis + zdis + mdis);
        }
        /// <summary>
        /// Equality between this object and another
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            if (!(other is Point))
                return false;

            return Equals((Point)other);
        }
        /// <summary>
        /// Equality between this object and another
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Point other)
        {
            return X == other.X && Y == other.Y && Z == other.Z && M == other.M;
        }
        /// <summary>
        /// HashCode.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return new { X, Y, Z, M }.GetHashCode();
        }
    }
}
