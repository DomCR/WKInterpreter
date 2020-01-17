using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WKInterpreter_initial
{
    public class Line : Geometry, IWKSerializable
    {
        [Obsolete]
        public List<Coordinate> Coords { get; set; }
        public List<Point> Points { get; set; }

        public Line(byte[] blop) : base(blop)
        {
            //Initialize list
            Coords = new List<Coordinate>();
            Points = new List<Point>();

            int nPts = BitConverter.ToInt32(extractBytes(blop, 5, 4), 0);
            int pos = 9;

            //Set the points in the line
            for (int i = 0; i < nPts; i++)
            {
                double x = BitConverter.ToDouble(extractBytes(blop, pos, 8), 0);
                double y = BitConverter.ToDouble(extractBytes(blop, pos + 8, 8), 0);
                Coords.Add(new Coordinate(x, y));
                pos += 16;
            }
        }

        public string ToWKT()
        {
            throw new NotImplementedException();
        }

        public byte[] ToWKB()
        {
            throw new NotImplementedException();
        }
    }
}