using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WKInterpreter.Extensions
{
    /// <summary>
    /// Extension methods for the DimensionType enum.
    /// </summary>
    public static class DimensionTypeExtensions
    {
        /// <summary>
        /// Return all the dimension types in it's wkt form. 
        /// </summary>
        /// <returns></returns>
        public static string[] GetEnumTypes()
        {
            return Enum.GetValues(typeof(DimensionType)).Cast<DimensionType>().Select(o => o.WktEncode()).ToArray();
        }
        /// <summary>
        /// Parse a dimension type from WKT.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DimensionType Parse(string str)
        {
            String.IsNullOrEmpty(str);
            switch (str)
            {
                case "":
                case null:
                    return DimensionType.XY;
                case "Z": return DimensionType.XYZ;
                case "M": return DimensionType.XYM;
                case "ZM": return DimensionType.XYZM;
                default:
                    throw new NotSupportedException(str);
            }
        }
        /// <summary>
        /// Parse a int into a dimension and a geometry type.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DimensionType Parse(int value, out GeometryType type)
        {
            int tmp = value;
            type = GeometryType.GEOMETRY;
            DimensionType dim = DimensionType.XY;

            for (int i = 0; i < GetEnumTypes().Count(); i++)
            {
                if (tmp % 1000 == 0)
                {
                    tmp -= 1000;
                }
                else
                {
                    type = (GeometryType)(tmp % 1000);
                    dim = (DimensionType)(value - (tmp % 1000));
                }
            }

            return dim;
        }
        public static int GetDimensionValue(this DimensionType dimension)
        {
            switch (dimension)
            {
                case DimensionType.XY:
                    return 2;
                case DimensionType.XYZ:
                case DimensionType.XYM:
                    return 3;
                case DimensionType.XYZM:
                    return 4;
                default:
                    return -1;
            }
        }
        /// <summary>
        /// Codification encode for WKT.
        /// </summary>
        /// <param name="dimension"></param>
        /// <returns></returns>
        public static string WktEncode(this DimensionType dimension)
        {
            switch (dimension)
            {
                case DimensionType.XY:
                    return "";
                case DimensionType.XYZ:
                    return "Z";
                case DimensionType.XYM:
                    return "M";
                case DimensionType.XYZM:
                    return "ZM";
                default:
                    throw new NotImplementedException("WktEncode not implemented for the DimensinoType: " + dimension.ToString());
            }
        }
    }
}
