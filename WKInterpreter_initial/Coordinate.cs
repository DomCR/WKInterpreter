﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WKInterpreter_initial.Enums;
using WKInterpreter_initial.Exceptions;

namespace WKInterpreter_initial
{
    /// <summary>
    /// Represents a coordinate in the space.
    /// </summary>
    //Use only the Point geometry class
    [Obsolete]
    public class Coordinate : IWKSerializable
    {
        /// <summary>
        /// Longitude of the coordinate.
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Latitude of the coordinate.
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Altitude of the coordinate.
        /// </summary>
        public double Altitude { get; set; }
        /// <summary>
        /// Coordinate type
        /// </summary>
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
        /// Parse a coordinate using a string format.
        /// </summary>
        /// <example>
        /// ([double] [double])
        /// </example>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Coordinate Parse(string str)
        {
            Coordinate tmp = new Coordinate();
            string[] num = str.Replace("(", "").Replace(")", "").Split(' ');

            if(num.Length > 2)
                throw new WrongStringFormatException("Wrong string format, too many arguments.");

            try
            {
                tmp.Longitude = double.Parse(num[0]);
                tmp.Latitude = double.Parse(num[1]);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tmp;
        }
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
