using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WKInterpreter.Enums;

namespace WKInterpreter
{
    /// <summary>
    /// Represents a coordinate in the space.
    /// </summary>
    public class Coordinate : IWKSerializable
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Altitude { get; set; }
        public CoordinateType Type { get; private set; }

        /// <summary>
        /// Set an empty coordinate to the 0, 0, 0 point
        /// </summary>
        public Coordinate()
        {
            Latitude = 0.0d;
            Longitude = 0.0d;
            Altitude = 0.0d;
        }
        public Coordinate(double x, double y, double z)
        {
            Longitude = x;
            Latitude = y;
            Altitude = z;
        }
        public Coordinate(double x, double y)
        {
            Longitude = x;
            Latitude = y;
            Altitude = 0.0d;
        }
        //*********************************************************
        public bool IsNear(Coordinate other, double minDist = 0, bool ignoreZ = true)
        {
            double x = this.Longitude - other.Longitude;
            double y = this.Latitude - other.Latitude;
            double z = 0;

            if (!ignoreZ)
                z = this.Altitude - other.Altitude;

            double dist = Math.Sqrt(y * y + x * x + z * z);
            return dist <= minDist;
        }
        //*********************************************************
        /// <summary>
        /// Addition between coordinates, lat + lat0 | lon + lon0 | alt + alt0.
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static Coordinate operator +(Coordinate c1, Coordinate c2)
        {
            Coordinate tmpCoord = new Coordinate();

            tmpCoord.Latitude = c1.Latitude + c2.Latitude;
            tmpCoord.Longitude = c1.Longitude + c2.Longitude;
            tmpCoord.Altitude = c1.Altitude + c2.Altitude;

            return tmpCoord;
        }
        /// <summary>
        /// Subsctraction between coordinates, lat - lat0 | lon - lon0 | alt - alt0.
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static Coordinate operator -(Coordinate c1, Coordinate c2)
        {
            Coordinate tmpCoord = new Coordinate();

            tmpCoord.Latitude = c1.Latitude - c2.Latitude;
            tmpCoord.Longitude = c1.Longitude - c2.Longitude;
            tmpCoord.Altitude = c1.Altitude - c2.Altitude;

            return tmpCoord;
        }
        //*********************************************************
        public override string ToString()
        {
            return "(" + Longitude + ", " + Latitude + ", " + Altitude + ")";
        }
        public string ToWKT()
        {
            return "(" + Longitude + ", " + Latitude + ")";
        }
        public byte[] ToWKB()
        {
            throw new NotImplementedException();
        }
    }
}
