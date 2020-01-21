using System;
using System.Linq;
using WKInterpreter.Readers;

namespace WKInterpreter
{
    /// <summary>
    /// Represents a geometric object.
    /// </summary>
    public abstract class Geometry
    {
        public EndianType Endian { get; set; }

        /// <summary>
        /// Type of the geometry.
        /// </summary>
        public virtual GeometryType GeometryType { get { return GeometryType.GEOMETRY; } }
        public abstract DimensionType Dimension { get; }
        public abstract bool IsEmpty { get; }
        public abstract bool IsValid { get; }

        protected Geometry()
        {
            Endian = EndianType.BIG_ENDIAN;
        }
        //******************************************************************************************
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
            //return WkbReader.Read(blop);

            throw new NotImplementedException();
        }
        public static string TextSerialize()
        {
            throw new NotImplementedException();
        }
        public static byte[] BinarySerialize()
        {
            throw new NotImplementedException();
        }
        //******************************************************************************************
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