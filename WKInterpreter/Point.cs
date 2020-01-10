using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WKInterpreter.Geometries
{
    public class Point : Geometry
    {
        /// <summary>
        /// Coordinate point to store the variables
        /// </summary>
        public Coordinate Coord { get; set; }
        /// <summary>
        /// Equivalent to the Longitude in geospatial coords
        /// </summary>
        public double X { get { return Coord.X; } }
        /// <summary>
        /// Equivalent to the Latitude in geospatial coords
        /// </summary>
        public double Y { get { return Coord.Y; } }
        /// <summary>
        /// Equivalent to the Altitude in geospatial coords
        /// </summary>
        public double Z { get { return Coord.Z; } }

        /// <summary>
        /// Default constructor using only the blop
        /// </summary>
        /// <param name="blop"></param>
        public Point(byte[] blop) : base(blop)
        {
            int pos = 5;
            Coordinate tmp = new Coordinate();

            tmp.X = BitConverter.ToDouble(p_extractBytes(Blop, pos, 8), 0);
            tmp.Y = BitConverter.ToDouble(p_extractBytes(Blop, pos + 8, 8), 0);
            tmp.Z = 0.0d; //3D not implemented

            Coord = tmp;
        }

        /// <summary>
        /// Constructor with the Z coord
        /// </summary>
        /// <param name="blop"></param>
        /// <param name="setLevel"></param>
        public Point(byte[] blop, double setLevel) : this(blop)
        {
            Coord.Z = setLevel;
        }
        //*********************************************************
        public override string ToString()
        {
            return "POINT " + Coord.ToString();
        }
    }
}
