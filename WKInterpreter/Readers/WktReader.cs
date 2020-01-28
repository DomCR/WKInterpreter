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
                    return ReadPolygon(dimension);
                case GeometryType.MULTIPOINT:
                    return ReadMultiPoint(dimension);
                case GeometryType.MULTILINESTRING:
                    return ReadMultiLineString(dimension);
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
        public Point ReadPoint(DimensionType dimension)
        {
            //Get the string data information of the coordinate
            string coordinate = readGroup('(', ')', m_buffer, ref m_currIndex);

            //Read point
            return ReadCoordinate(dimension, coordinate);
        }
        public LineString ReadLineString(DimensionType dimension)
        {
            string[] linePoints = readGroup('(', ')', m_buffer, ref m_currIndex).Split(',');
            LineString line = new LineString();

            foreach (string pt in linePoints)
            {
                line.AddGeometry(ReadCoordinate(dimension, pt));
            }

            return line;
        }
        public Polygon ReadPolygon(DimensionType dimension)
        {
            throw new NotImplementedException();
        }
        public MultiPoint ReadMultiPoint(DimensionType dimension)
        {
            string[] points = readGroup('(', ')', m_buffer, ref m_currIndex).Split(',');
            MultiPoint multiPoint = new MultiPoint();

            foreach (string pt in points)
            {
                multiPoint.AddGeometry(ReadCoordinate(dimension, pt));
            }

            return multiPoint;
        }
        public MultiLineString ReadMultiLineString(DimensionType dimension)
        {
            string groups = readGroup('(', ')', m_buffer, ref m_currIndex);
            string[] lines = readSubGroups('(', ')', groups);
            MultiLineString multiLine = new MultiLineString();

            foreach (string line in lines)
            {
                LineString tmpLine = new LineString();
                string[] points = line.Replace("(", "").Replace(")", "").Split(',');
                foreach (string point in points)
                {
                    tmpLine.AddGeometry(ReadCoordinate(dimension, point));
                }
                multiLine.AddGeometry(tmpLine);
            }
            return multiLine;
        }
        public MultiPolygon ReadMultiPolygon(DimensionType dimension)
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
        /// <summary>
        /// Read a string and return the first string enclosed between the selected characters.
        /// </summary>
        /// <param name="open"></param>
        /// <param name="close"></param>
        /// <param name="line"></param>
        /// <param name="lastIndex">Index of the closing character.</param>
        /// <returns>The string between the 2 tokens.</returns>
        private string readGroup(char open, char close, string line, ref int lastIndex)
        {
            var stack = new Stack<int>();
            bool isFirst = true;
            string group = "";

            for (int i = lastIndex; i < line.Length; i++)
            {
                if (line[i] == open)
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
                else if (line[i] == close)
                {
                    //Check if the sequence contains an open
                    if (!isFirst)
                    {
                        stack.Pop();

                        //Closing character found
                        if (!stack.Any())
                        {
                            lastIndex = i;
                            break;
                        }
                    }
                }

                //If the first open character have been found, start reading the string
                if (!isFirst)
                {
                    group += line[i];
                }
            }

            return group;
        }
        private string[] readSubGroups(char open, char close, string groups)
        {
            List<string> subGroups = new List<string>();
            string currGroup = "";

            int index = 0;
            while (!String.IsNullOrEmpty(currGroup = readGroup(open, close, groups, ref index)))
            {
                subGroups.Add(currGroup);
            }

            return subGroups.ToArray();
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
                }
            }

            //Token not found
            if (String.IsNullOrEmpty(token))
                return null;

            m_currIndex = pos.GetValueOrDefault() + token.Length;

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
