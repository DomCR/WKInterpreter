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
        XY = 0x0000,
        XYZ = 0x03E8,
        XYM = 0x07D0,
        XYZM = 0x0BB8
    }

    public static class DimensionTypeExtension
    {
        public static string[] GetEnumTypes()
        {
            return Enum.GetValues(typeof(DimensionType)).Cast<DimensionType>().Select(o => o.WktEncode()).ToArray();
        }
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
