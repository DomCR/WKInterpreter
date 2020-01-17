using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WKInterpreter_initial
{
    public class MultiLine : Geometry
    {
        //TODO: Change the line for a list of polylines
        public List<Line> Lines { get; set; }

        public MultiLine(byte[] blop) : base(blop)
        {
            Lines = new List<Line>();

            int nLines = BitConverter.ToInt32(extractBytes(blop, 5, 4), 0);
            int pos = 9;

            for (int i = 0; i < nLines; i++)
            {
                //1 - endian + 4 - Category + 4 nPoints * 16 (8 byte coord)
                int length = 9;
                int nPts = BitConverter.ToInt32(extractBytes(blop, pos + 5, 4), 0);
                length += nPts * 16;

                byte[] lnBlop = new byte[length];
                Array.Copy(blop, pos, lnBlop, 0, length);
                Lines.Add(new Line(lnBlop));

                pos += length;
            }
        }
        //*********************************************************
    }
}
