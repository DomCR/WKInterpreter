using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WKInterpreter
{
    /// <summary>
    /// Defines the point dimensions.
    /// </summary>
    public enum DimensionType
    {
        /// <summary>
        /// 2D dimension, integer value 0.
        /// </summary>
        XY = 0x0000,
        /// <summary>
        /// 3D dimension using the Z component, integer value 1000.
        /// </summary>
        XYZ = 0x03E8,
        /// <summary>
        /// 3D dimension using the M component, integer value 2000.
        /// </summary>
        XYM = 0x07D0,
        /// <summary>
        /// 4D dimension, integer value 3000.
        /// </summary>
        XYZM = 0x0BB8
    }
    /// <summary>
    /// Extension methods for the DimensionType enum.
    /// </summary>
    public static class DimensionTypeExtension
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
