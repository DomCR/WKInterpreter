using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WKInterpreter.CMD.TestData
{
    public class BinaryArray
    {
        public List<byte> BigEndian { get; set; }
        public List<byte> LittleEndian { get; set; }

        public BinaryArray()
        {
            BigEndian = new List<byte>();
            LittleEndian = new List<byte>();

            BigEndian.Add(0);
            LittleEndian.Add(1);
        }

        public void AddBytes(byte b)
        {
            BigEndian.Add(b);
            LittleEndian.Add(b);
        }
        public void AddBytes(byte[] bytes)
        {
            BigEndian.AddRange(bytes);
            LittleEndian.AddRange(bytes.Reverse());
        }
    }
}
