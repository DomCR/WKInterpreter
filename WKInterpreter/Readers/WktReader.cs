using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WKInterpreter.Readers
{
    internal class WktReader : IReader
    {
        private string[] m_geometryTypes = Enum.GetValues(typeof(GeometryType)).Cast<GeometryType>().Select(o => o.ToString()).ToArray();
        private string[] m_dimensions = DimensionTypeExtension.GetEnumTypes();
        private int? m_currIndex = null;
        private string m_buffer;

        public WktReader(string line)
        {
            m_buffer = line.ToUpper();
        }
        public Geometry Read()
        {
            m_currIndex = 0;

            if (readUntilToken("SRID") == "")
                throw new NotImplementedException();

            GeometryType geometryType = readUntilToken<GeometryType>(m_geometryTypes);
            DimensionType dimension = ReadDimension(); //Not necessary if the geometry is not empty

            //If the line contains the empty token, return an empty geometry
            if (IsEmpty(m_buffer))
                return CreateGeometry(geometryType);

            return Read(geometryType);
        }
        public Geometry Read(GeometryType geometryType)
        {
            switch (geometryType)
            {
                case GeometryType.GEOMETRY:
                    throw new NotSupportedException(geometryType.ToString());
                case GeometryType.POINT:
                    return ReadPoint();
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

            throw new NotImplementedException();
        }
        public DimensionType ReadDimension()
        {
            string dim = readUntilToken(m_dimensions);

            switch (dim)
            {
                case null: return DimensionType.XY;
                case "Z": return DimensionType.XYZ;
                case "M": return DimensionType.XYM;
                case "ZM": return DimensionType.XYZM;
                default:
                    throw new NotSupportedException(dim);
            }
        }
        public Point ReadCoordinate()
        {
            int initial = m_currIndex.GetValueOrDefault();
            readUntil(')');
            string[] svalues = m_buffer.Substring(initial, m_currIndex.GetValueOrDefault() - initial - 1).Split(' ');
            double[] dvalues = new double[svalues.Length];

            for (int i = 0; i < svalues.Length; i++)
            {
                if (double.TryParse(svalues[i], out double value))
                {
                    dvalues[i] = value;
                }
                else
                {
                    throw new Exception("Error parsing a numeric value near the index " + m_currIndex);
                }
            }

            throw new NotImplementedException();
        }
        public Point ReadPoint()
        {
            readUntil('(');

            //Read point
            ReadCoordinate();

            //readUntil(')');

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
        public bool IsEmpty(string txt)
        {
            return txt.Contains("EMPTY", StringComparison.OrdinalIgnoreCase);
        }
        public void Dispose()
        {
            m_geometryTypes = null;
            m_dimensions = null;
            m_buffer = null;
        }
        //*********************************************************************************
        /// <summary>
        /// Read until finds the first token.
        /// </summary>
        /// <remarks>
        /// This method advances the current index position.
        /// </remarks>
        /// <param name="tokens"></param>
        /// <returns>Return the found token.</returns>
        private string readUntilToken(params string[] tokens)
        {
            string line = m_buffer.Substring(m_currIndex.GetValueOrDefault());
            string token = null;
            int? pos = null;

            foreach (string item in tokens)
            {
                //Ignore empty tokens 
                if (String.IsNullOrEmpty(item))
                    continue;

                int curr = line.IndexOf(item);

                //Get the next token in the buffer
                if ((pos == null || curr < pos) && curr >= m_currIndex)
                {
                    pos = line.IndexOf(item);
                    token = item;
                    m_currIndex = pos + item.Length;
                }
            }

            return token;
        }
        /// <summary>
        /// Read until finds the first token.
        /// </summary>
        /// <remarks>
        /// This method advances the current index position.
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="tokens"></param>
        /// <returns>Return the found token.</returns>
        private T readUntilToken<T>(params string[] tokens) where T : struct
        {
            string token = readUntilToken(tokens);

            //Validate the token
            if (String.IsNullOrEmpty(token))
                throw new Exception("Expecting token after index: " + m_currIndex);
            if (!Enum.TryParse<T>(token, out T type))
                throw new ArgumentException("Token not match with enum after index: " + m_currIndex);

            return type;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        private void readUntil(char match)
        {
            for (int i = m_currIndex.GetValueOrDefault(); i < m_buffer.Length; i++)
            {
                if (m_buffer[i] == match)
                {
                    m_currIndex = i + 1;
                    return;
                }
            }

            throw new Exception("Expecting '" + match + "' after index: " + m_currIndex);
        }
    }
}
