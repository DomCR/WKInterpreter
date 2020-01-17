using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WKInterpreter_initial.Enums;
using WKInterpreter_initial.Exceptions;

namespace WKInterpreter_initial
{
    /// <summary>
    /// Represents a geometric object.
    /// </summary>
    public abstract class Geometry
    {
        public EndianEncode Endian { get; set; }
        public GeometryType GeometryType { get; private set; }

        public Geometry(byte[] blop)
        {
            this.Endian = (EndianEncode)extractBytes(blop, 0, 1).First();
            this.GeometryType = (GeometryType)BitConverter.ToInt32(extractBytes(blop, 1, 4), 0);
        }
        protected Geometry(string str)
        {
            Endian = EndianEncode.BIG_ENDIAN;
        }
        //*********************************************************
        /// <summary>
        /// Return a geometry object from a binary blop
        /// </summary>
        /// <param name="blop"></param>
        /// <returns></returns>
        public static Geometry Deserialize(byte[] blop)
        {
            //EndianEncode codeType = (EndianEncode)blop[0];
            GeometryType type = (GeometryType)BitConverter.ToInt32(extractBytes(blop, 1, 4), 0);

            switch (type)
            {
                case GeometryType.NULL:
                    break;
                case GeometryType.POINT:
                    //01 01000000 8-byte x-coord 8-byte y-coord
                    return new Point(blop);
                case GeometryType.LINESTRING:
                    //01 02000000 03000000 POINT COORDS
                    //2  category nPts
                    return new Line(blop);
                case GeometryType.POLYGON:
                    throw new NotImplementedException();
                case GeometryType.MULTIPOINT:
                    throw new NotImplementedException();
                case GeometryType.MULTILINESTRING:
                    //01 05000000 02000000 01 02000000 03000000 POINT COORDS (some line code 01 02000000 04000000)
                    //2  category nlines   en line     nPoints                               -      NEW LINE    -
                    return new MultiLine(blop);
                case GeometryType.MULTIPOLYGON:
                case GeometryType.GEOMETRYCOLLECTION:
                case GeometryType.CIRCULARSTRING:
                case GeometryType.COMPOUNDCURVE:
                case GeometryType.CURVEPOLYGON:
                case GeometryType.MULTICURVE:
                case GeometryType.MULTISURFACE:
                case GeometryType.CURVE:
                case GeometryType.SURFACE:
                case GeometryType.POLYHEDRALSURFACE:
                case GeometryType.TIN:
                case GeometryType.TRIANGLE:
                case GeometryType.CIRCLE:
                case GeometryType.GEODESICSTRING:
                case GeometryType.ELLIPTICALCURVE:
                case GeometryType.URBSCURVE:
                case GeometryType.CLOTHOID:
                case GeometryType.SPIRALCURVE:
                case GeometryType.COMPOUNDSURFACE:
                case GeometryType.BREPSOLID:
                case GeometryType.AFFINEPLACEMENT:
                    throw new NotImplementedException();
                default:
                    return null;
            }

            return null;
        }
        /// <summary>
        /// Return a geometry object from a WKT string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Geometry Deserialize(string str)
        {
            string name = str.Split('(')[0];

            GeometryType type = GeometryType.NULL;
            try
            {
                type = (GeometryType)Enum.Parse(typeof(GeometryType), name);
            }
            catch (Exception ex)
            {
                throw new WrongStringFormatException("Wrong string format, geometry name not found.", ex);
            }

            switch (type)
            {
                case GeometryType.NULL:
                    return null;
                case GeometryType.POINT:
                    return new Point(str);
                case GeometryType.LINESTRING:
                    break;
                case GeometryType.POLYGON:
                    break;
                case GeometryType.MULTIPOINT:
                    break;
                case GeometryType.MULTILINESTRING:
                    break;
                case GeometryType.MULTIPOLYGON:
                    break;
                case GeometryType.GEOMETRYCOLLECTION:
                    break;
                case GeometryType.CIRCULARSTRING:
                    break;
                case GeometryType.COMPOUNDCURVE:
                    break;
                case GeometryType.CURVEPOLYGON:
                    break;
                case GeometryType.MULTICURVE:
                    break;
                case GeometryType.MULTISURFACE:
                    break;
                case GeometryType.CURVE:
                    break;
                case GeometryType.SURFACE:
                    break;
                case GeometryType.POLYHEDRALSURFACE:
                    break;
                case GeometryType.TIN:
                    break;
                case GeometryType.TRIANGLE:
                    break;
                case GeometryType.CIRCLE:
                    break;
                case GeometryType.GEODESICSTRING:
                    break;
                case GeometryType.ELLIPTICALCURVE:
                    break;
                case GeometryType.URBSCURVE:
                    break;
                case GeometryType.CLOTHOID:
                    break;
                case GeometryType.SPIRALCURVE:
                    break;
                case GeometryType.COMPOUNDSURFACE:
                    break;
                case GeometryType.BREPSOLID:
                    break;
                case GeometryType.AFFINEPLACEMENT:
                    break;
                default:
                    break;
            }

            throw new NotImplementedException();
        }
        //*********************************************************
        /// <summary>
        /// Extract the bytes from an array of INTs
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        protected static byte[] extractBytes(byte[] buffer, int start, int length, bool reverse = false)
        {
            byte[] result = new byte[length];
            Array.Copy(buffer, start, result, 0, length);

            return reverse ? result.Reverse().ToArray() : result;
        }
    }
}
