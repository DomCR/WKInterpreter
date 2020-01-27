using System;
using System.IO;
using System.Linq;
using WKInterpreter.Readers;

namespace WKInterpreter
{
    /// <summary>
    /// Represents a geometric object.
    /// </summary>
    public abstract class Geometry
    {
        /// <summary>
        /// Endiand codification.
        /// </summary>
        public EndianType Endian { get; set; }
        public int? Srid { get; set; }
        /// <summary>
        /// Type of the geometry.
        /// </summary>
        public virtual GeometryType GeometryType { get { return GeometryType.GEOMETRY; } }
        public abstract DimensionType Dimension { get; }
        public abstract bool IsEmpty { get; }
        public abstract bool IsValid { get; }
        /// <summary>
        /// Default constructor for an empty geometry.
        /// </summary>
        protected Geometry()
        {
            Srid = null;
        }
        //******************************************************************************************
        public static Geometry Deserialize<T>(Stream stream) where T : IReader
        {

            throw new NotImplementedException();
        }
        /// <summary>
        /// Reads a Well-Known-Text geometry.
        /// </summary>
        /// <example>
        /// Format: [geometry] [dimension] [empty?] ([geometric_information])
        /// </example>
        /// <remarks>
        /// Not implemented: [SRID];[WKT]
        /// </remarks>
        /// <param name="str"></param>
        public static Geometry Deserialize(string str)
        {
            Geometry value = null;

            using (WktReader reader = new WktReader(str))
            {
                value = reader.Read();
            }

            return value;
        }
        public static Geometry Deserialize(byte[] blop)
        {
            Geometry value = null;

            using (WkbReader reader = new WkbReader(blop))
            {
                value = reader.Read();
            }

            return value;
        }
        public static string TextSerialize()
        {
            throw new NotImplementedException();
        }
        public static byte[] BinarySerialize()
        {
            throw new NotImplementedException();
        }
    }
}