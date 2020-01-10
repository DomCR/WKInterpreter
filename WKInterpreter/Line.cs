using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WKInterpreter.Geometries
{
    public class Line : Geometry
    {
        //*********************************************************
        public List<Coordinate> Coords { get; set; }
        public int nCoords { get { return Coords.Count; } }

        public Line(byte[] blop) : base(blop)
        {
            //Initialize list
            Coords = new List<Coordinate>();

            int nPts = BitConverter.ToInt32(p_extractBytes(Blop, 5, 4), 0);
            int pos = 9;

            //Set the points in the line
            for (int i = 0; i < nPts; i++)
            {
                double x = BitConverter.ToDouble(p_extractBytes(Blop, pos, 8), 0);
                double y = BitConverter.ToDouble(p_extractBytes(Blop, pos + 8, 8), 0);
                Coords.Add(new Coordinate(x, y));
                pos += 16;
            }
        }
        //*********************************************************
    }
}
