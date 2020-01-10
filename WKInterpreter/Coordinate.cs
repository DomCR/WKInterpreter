using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WKInterpreter.Enums;

namespace WKInterpreter
{
    public class Coordinate
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public CoordinateType Type { get; private set; }

        public Coordinate()
        {
            X = 0.0d;
            Y = 0.0d;
            Z = 0.0d;
        }
        public Coordinate(double p1, double p2, double p3)
        {
            X = p1;
            Y = p2;
            Z = p3;
        }
        public Coordinate(double p1, double p2)
        {
            X = p1;
            Y = p2;
            Z = 0.0d;
        }
        //*********************************************************
        public bool IsNear(Coordinate other, double minDist = 0, bool ignoreZ = true)
        {
            double Vect_X = this.X - other.X;
            double Vect_Y = this.Y - other.Y;
            double Vect_Z = 0;

            if (!ignoreZ)
                Vect_Z = this.Z - other.Z;

            double dist = Math.Sqrt(Vect_X * Vect_X + Vect_Y * Vect_Y + Vect_Z * Vect_Z);
            return dist <= minDist;
        }
        //*********************************************************
        public static Coordinate operator +(Coordinate c1, Coordinate c2)
        {
            Coordinate tmpCoord = new Coordinate();

            tmpCoord.X = c1.X + c2.X;
            tmpCoord.Y = c1.Y + c2.Y;
            tmpCoord.Z = c1.Z + c2.Z;

            return tmpCoord;
        }
        public static Coordinate operator -(Coordinate c1, Coordinate c2)
        {
            Coordinate tmpCoord = new Coordinate();

            tmpCoord.X = c1.X - c2.X;
            tmpCoord.Y = c1.Y - c2.Y;
            tmpCoord.Z = c1.Z - c2.Z;

            return tmpCoord;
        }
        //*********************************************************
        public override string ToString()
        {
            return "(" + X + ", " + Y + ", " + Z + ")";
        }
    }
}
