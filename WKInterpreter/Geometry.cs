using System;
using System.Linq;

namespace WKInterpreter
{
    /// <summary>
    /// Represents a geometric object.
    /// </summary>
    public abstract class Geometry
    {
        public EndianType Endian { get; set; }
        public DimensionType Dimension { get; protected set; }

        /// <summary>
        /// Type of the geometry.
        /// </summary>
        public virtual GeometryType GeometryType { get { return GeometryType.GEOMETRY; } }

        protected Geometry()
        {
            Endian = EndianType.BIG_ENDIAN;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="blop"></param>
        protected Geometry(byte[] blop)
        {
            this.Endian = (EndianType)extractBytes(blop, 0, 1).First();
            //this.GeometryType = (GeometryType)BitConverter.ToInt32(extractBytes(blop, 1, 4), 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        protected Geometry(string str) : this()
        {

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