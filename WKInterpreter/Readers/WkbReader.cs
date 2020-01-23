using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WKInterpreter.Extensions;

namespace WKInterpreter.Readers
{
    internal class WkbReader : IReader
    {
        private byte[] m_blop;
        private EndianType m_endian;
        private int m_index;

        public WkbReader(byte[] blop)
        {
            m_blop = blop;
        }
        public Geometry Read()
        {
            //Initialize reader
            m_index = 0;

            m_endian = BitConverter.ToBoolean(extractBytes(m_index, 1, ref m_index), 0) ? EndianType.LITTLE_ENDIAN : EndianType.BIG_ENDIAN;
            DimensionType dimension = DimensionTypeExtension.Parse(ReadNextInt(), out GeometryType geometryType);
            return Read(geometryType, dimension);
        }
        public Geometry Read(GeometryType geometryType, DimensionType dimension)
        {
            switch (geometryType)
            {
                case GeometryType.GEOMETRY:
                    throw new NotSupportedException(geometryType.ToString());
                case GeometryType.POINT:
                    return ReadPoint(dimension);
                case GeometryType.LINESTRING:
                    return ReadLineString(dimension);
                case GeometryType.POLYGON:
                    return ReadPolygon(dimension);
                case GeometryType.MULTIPOINT:
                case GeometryType.MULTILINESTRING:
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
                default:
                    throw new NotSupportedException(geometryType.ToString());
            }

            throw new NotImplementedException();
        }
        public Geometry CreateGeometry(GeometryType geometryType)
        {
            switch (geometryType)
            {
                case GeometryType.GEOMETRY:
                    throw new NotSupportedException(geometryType.ToString());
                case GeometryType.POINT:
                    return new Point();
                case GeometryType.LINESTRING:
                case GeometryType.POLYGON:
                case GeometryType.MULTIPOINT:
                case GeometryType.MULTILINESTRING:
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
                default:
                    throw new NotSupportedException(geometryType.ToString());
            }
        }
        public Point ReadPoint(DimensionType dimension)
        {
            switch (dimension)
            {
                case DimensionType.XY:
                    return new Point(ReadNextDouble(), ReadNextDouble());
                case DimensionType.XYZ:
                    return new Point(ReadNextDouble(), ReadNextDouble(), ReadNextDouble());
                case DimensionType.XYM:
                    return new Point(ReadNextDouble(), ReadNextDouble(), null, ReadNextDouble());
                case DimensionType.XYZM:
                    return new Point(ReadNextDouble(), ReadNextDouble(), ReadNextDouble(), ReadNextDouble());
                default:
                    throw new NotSupportedException(dimension.ToString());
            }
        }
        public LineString ReadLineString(DimensionType dimension)
        {
            //read the n points

            //for to create n points inside the line

            throw new NotImplementedException();
        }
        public Polygon ReadPolygon(DimensionType dimension)
        {
            throw new NotImplementedException();
        }
        public MultiPoint ReadMultiPoint(DimensionType dimension)
        {
            throw new NotImplementedException();
        }
        public MultiLineString ReadMultiLineString(DimensionType dimension)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Read the next int in the byte array.
        /// This method advances the current index position.
        /// </summary>
        /// <returns></returns>
        public int ReadNextInt()
        {
            return BitConverter.ToInt32(extractBytes(m_index, 4, ref m_index));
        }
        /// <summary>
        /// Read the next double in the byte array.
        /// This method advances the current index position.
        /// </summary>
        /// <returns></returns>
        public double ReadNextDouble()
        {
            return BitConverter.ToDouble(extractBytes(m_index, 8, ref m_index));
        }
        public void Dispose()
        {
            m_blop = null;
        }
        //*********************************************************************************
        /// <summary>
        /// Extract a number of bytes from the current array.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="lastIndex"></param>
        /// <returns></returns>
        private byte[] extractBytes(int start, int length, ref int lastIndex)
        {
            //Extract the selected bytes
            byte[] result = new byte[length];
            Array.Copy(m_blop, start, result, 0, length);
            lastIndex = start + length;

            //Check the endian of the machine
            if (BitConverter.IsLittleEndian)
                result = result.Reverse().ToArray();

            //Reverse the array if is in little endian
            return m_endian == EndianType.LITTLE_ENDIAN ? result.Reverse().ToArray() : result;
        }
    }
}
