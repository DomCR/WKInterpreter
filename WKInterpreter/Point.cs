using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WKInterpreter
{
    public class Point : Geometry, IWKSerializable
    {
        /// <summary>
        /// Coordinate point to store the variables
        /// </summary>
        public Coordinate Coord { get; set; }
        /// <summary>
        /// Equivalent to the Longitude in geospatial coords
        /// </summary>
        public double X { get { return Coord.Longitude; } }
        /// <summary>
        /// Equivalent to the Latitude in geospatial coords
        /// </summary>
        public double Y { get { return Coord.Latitude; } }
        /// <summary>
        /// Equivalent to the Altitude in geospatial coords
        /// </summary>
        public double Z { get { return Coord.Altitude; } }

        /// <summary>
        /// Default constructor using only the blop
        /// </summary>
        /// <param name="blop"></param>
        public Point(byte[] blop) : base(blop)
        {
            int pos = 5;
            Coordinate tmp = new Coordinate();

            tmp.Longitude = BitConverter.ToDouble(extractBytes(m_blop, pos, 8), 0);
            tmp.Latitude = BitConverter.ToDouble(extractBytes(m_blop, pos + 8, 8), 0);
            tmp.Altitude = 0.0d; //3D not implemented

            Coord = tmp;
        }

        /// <summary>
        /// Constructor with the Z coord
        /// </summary>
        /// <param name="blop"></param>
        /// <param name="setLevel"></param>
        public Point(byte[] blop, double setLevel) : this(blop)
        {
            Coord.Altitude = setLevel;
        }
        //*********************************************************
        public string ToWKT()
        {
            return "POINT" + Coord.ToString();
        }
        public byte[] ToWKB()
        {
            throw new NotImplementedException();
        }
        //*********************************************************
        public override string ToString()
        {
            return Coord.ToString();
        }
    }
}
