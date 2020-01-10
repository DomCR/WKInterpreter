using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WKInterpreter.Emuns;

namespace WKInterpreter
{
    /// <summary>
    /// Represents a geometric object
    /// </summary>
    public abstract class Geometry
    {
        public EndianEncode Endian { get; set; }
        public GeometryType GeometryType { get; set; }
        protected byte[] m_blop;

        public Geometry(byte[] blop)
        {
            this.m_blop = blop;
            this.Endian = (EndianEncode)extractBytes(m_blop, 0, 1).First();
            this.GeometryType = (GeometryType)BitConverter.ToInt32(extractBytes(m_blop, 1, 4), 0);
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
