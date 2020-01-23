using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WKInterpreter.Extensions;

namespace WKInterpreter.Readers
{
    internal class WktReader : IReader
    {
        private string[] m_geometryTypes = Enum.GetValues(typeof(GeometryType)).Cast<GeometryType>().Select(o => o.ToString()).ToArray();
        private string[] m_dimensions = DimensionTypeExtension.GetEnumTypes();
        private int m_currIndex;
        private string m_buffer;
        /// <summary>
        /// Reads a Well-Known-Text geometry.
        /// </summary>
        /// <example>
        /// Format: [geometry] [dimension] [empty?] ([geometric_information])
        /// </example>
        /// <param name="line"></param>
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
            DimensionType dimension = ReadDimension();

            //If the line contains the empty token, return an empty geometry
            if (IsEmpty())
                return CreateEmpty(geometryType);

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
        public Geometry CreateEmpty(GeometryType geometryType)
        {
            switch (geometryType)
            {
                case GeometryType.GEOMETRY:
                    throw new NotSupportedException(geometryType.ToString());
                case GeometryType.POINT:
                    return new Point();
                case GeometryType.LINESTRING:
                    return new LineString();
                case GeometryType.POLYGON:
                    return new Polygon();
                case GeometryType.MULTIPOINT:
                    return new MultiPoint();
                case GeometryType.MULTILINESTRING:
                    return new MultiLineString();
                case GeometryType.MULTIPOLYGON:
                    //return new Multip
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
        /// <summary>
        /// Read the dimension of the Geometry.
        /// </summary>
        /// <returns></returns>
        public DimensionType ReadDimension()
        {
            string dim = readUntilToken(m_dimensions.Reverse().ToArray());
            return DimensionTypeExtension.Parse(dim);
        }
        public Point ReadCoordinate(DimensionType dimension, string coord)
        {
            string[] svalues = coord.Split(' ');
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

            //Validate the readed values
            if (dimension.GetDimensionValue() != dvalues.Length)
                throw new Exception("Wrong number of arguments, found " + dvalues.Length + " expecting " + dimension.GetDimensionValue());

            //Point to store values
            Point pt = new Point();
            
            switch (dimension)
            {
                case DimensionType.XY:
                    //Setup the values
                    pt.X = dvalues[0];
                    pt.Y = dvalues[1];
                    break;
                case DimensionType.XYZ:
                    //Setup the values
                    pt.X = dvalues[0];
                    pt.Y = dvalues[1];
                    pt.Z = dvalues[2];
                    break;
                case DimensionType.XYM:
                    //Setup the values
                    pt.X = dvalues[0];
                    pt.Y = dvalues[1];
                    pt.M = dvalues[2];
                    break;
                case DimensionType.XYZM:
                    //Setup the values
                    pt.X = dvalues[0];
                    pt.Y = dvalues[1];
                    pt.Z = dvalues[2];
                    pt.M = dvalues[3];
                    break;
                default:
                    break;
            }

            return pt;
        }
        public Point ReadCoordinate(string coord)
        {
            string[] svalues = coord.Split(' ');
            double?[] dvalues = new double?[] { null, null, null, null };

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

            return new Point(dvalues[0], dvalues[1], dvalues[2], dvalues[3]);
        }
        public Point ReadPoint(DimensionType dimension)
        {
            //Get the string data information of the coordinate
            string coordinate = readGroup('(', ')', ref m_currIndex);

            //Read point
            return ReadCoordinate(dimension, coordinate);
        }
        public LineString ReadLineString(DimensionType dimension)
        {
            string[] linePoints = readGroup('(', ')', ref m_currIndex).Split(',');
            LineString line = new LineString();

            foreach (string pt in linePoints)
            {
                line.AddPoint(ReadCoordinate(dimension, pt));
            }

            return line;
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
        public bool IsEmpty()
        {
            if (tryReadUntil('(', out string token))
            {
                if (token.Contains("EMPTY"))
                    return true;
                else
                    return false;
            }

            return true;
        }
        public void Dispose()
        {
            m_geometryTypes = null;
            m_dimensions = null;
            m_buffer = null;
        }
        //*********************************************************************************
        private void skipWhitespaces()
        {
            while (m_currIndex < m_buffer.Length &&
                m_buffer[m_currIndex] == ' ')
            {
                m_currIndex++;
            }
        }
        /// <summary>
        /// Read a string and return the first string enclosed between the selected characters.
        /// </summary>
        /// <param name="open"></param>
        /// <param name="close"></param>
        /// <param name="lasIndex">Index of the closing character.</param>
        /// <returns>The string between the 2 tokens.</returns>
        private string readGroup(char open, char close, ref int lasIndex)
        {
            var stack = new Stack<int>();
            bool isFirst = true;
            string group = "";

            for (int i = m_currIndex; i < m_buffer.Length; i++)
            {
                if (m_buffer[i] == open)
                {
                    //Save the index of the open character
                    stack.Push(i);

                    //Save the index of the first open char
                    if (isFirst)
                    {
                        isFirst = false;
                        continue;
                    }
                }
                else if (m_buffer[i] == close)
                {
                    //Check if the sequence contains an open
                    if (!isFirst)
                    {
                        stack.Pop();

                        //Closing character found
                        if (!stack.Any())
                        {
                            lasIndex = i;
                            break;
                        }
                    }
                }

                //If the first open character have been found, start reading the string
                if (!isFirst)
                {
                    group += m_buffer[i];
                }
            }

            return group;
        }
        /// <summary>
        /// Read until finds the first token.
        /// This method advances the current index position.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns>Return the found token.</returns>
        private string readUntilToken(params string[] tokens)
        {
            string token = null;
            int? pos = null;

            foreach (string item in tokens)
            {
                //Ignore empty tokens 
                if (String.IsNullOrEmpty(item))
                    continue;

                int curr = m_buffer.IndexOf(item);

                //Get the next token in the buffer
                if ((pos == null || curr < pos) && curr >= m_currIndex)
                {
                    pos = m_buffer.IndexOf(item);
                    token = item;
                    m_currIndex = pos.GetValueOrDefault() + item.Length;
                }
            }

            return token;
        }
        /// <summary>
        /// Read until finds the first token.
        /// This method advances the current index position.
        /// </summary>
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
        /// Reads until a match is found, returns the substracted string.
        /// This method advances the current index position.
        /// </summary>
        /// <param name="match"></param>
        /// <param name="jumpToken"></param>
        /// <returns></returns>
        [Obsolete("Not structured method, must be deleted.")]
        private string readUntil(char match, bool jumpToken = false)
        {
            for (int i = m_currIndex; i < m_buffer.Length; i++)
            {
                if (m_buffer[i] == match)
                {
                    string substring = m_buffer.Substring(m_currIndex, i - m_currIndex);
                    m_currIndex = jumpToken ? i + 1 : i;
                    return substring;
                }
            }

            throw new Exception("Expecting '" + match + "' after index: " + m_currIndex);
        }
        /// <summary>
        /// Try to read until the match, returns the substring between the match and the current index.
        /// This method advances the current index position.
        /// </summary>
        /// <param name="match"></param>
        /// <param name="str">Substring between the match and the current index. Null if the match isn't found.</param>
        /// <returns></returns>
        [Obsolete("Not structured method, must be deleted.")]
        private bool tryReadUntil(char match, out string str)
        {
            try
            {
                str = readUntil(match);
                return true;
            }
            catch (Exception)
            {
                str = null;
                return false;
            }
        }
    }
}
