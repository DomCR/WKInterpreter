using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using WKInterpreter.Extensions;

namespace WKInterpreter.CMD.TestData
{
    public class Test
    {
        public string Id { get; set; }
        public GeometryType Type { get; set; }
        public DimensionType Dimension { get; set; }
        public string wkt { get; set; }
        public byte[] wkb_big { get; set; }
        public byte[] wkb_little { get; set; }
        public string ewkt { get; set; }
        public byte[] ewkb_big { get; set; }
        public byte[] ewkb_little { get; set; }
        public Geometry Validation { get; set; }

        private readonly Random m_random = new Random(Generator.Seed);
        private BinaryArray m_arr;

        public Test()
        {

        }
        public void CreateEmpty(GeometryType geometry, DimensionType dimension)
        {
            this.Type = geometry;
            this.Dimension = dimension;

            //Array to store the byte form
            m_arr = new BinaryArray();
            m_arr.AddBytes(intToBytes((int)geometry + (int)dimension));

            this.wkt = geometry.ToString() + " " + dimension.WktEncode() + " EMPTY";
            this.ewkt = "SRID=4326;" + geometry.ToString() + " " + dimension.WktEncode() + " EMPTY";

            this.wkb_big = m_arr.BigEndian.ToArray();
            this.wkb_little = m_arr.LittleEndian.ToArray();

            this.Validation = Activator.CreateInstance(geometry.GetEquivalentType()) as Geometry;
        }
        public void CreatePointTest(DimensionType dimension, bool negative)
        {
            this.Type = GeometryType.POINT;
            this.Dimension = dimension;
            m_arr = new BinaryArray();
            m_arr.AddBytes(intToBytes((int)this.Type + (int)dimension));

            this.wkt = "POINT " + dimension.WktEncode() + "(";
            this.ewkt = "SRID=4326;" + this.Type.ToString() + " " + dimension.WktEncode() + "(";

            addCoordinate(dimension, out Point pt, negative);

            this.wkt += ")";
            this.ewkt += ")";
            this.wkb_big = m_arr.BigEndian.ToArray();
            this.wkb_little = m_arr.LittleEndian.ToArray();

            Validation = pt;
        }
        public void CreateLineStringTest(DimensionType dimension, bool negative)
        {
            this.Type = GeometryType.LINESTRING;
            this.Dimension = dimension;
            m_arr = new BinaryArray();
            m_arr.AddBytes(intToBytes((int)this.Type + (int)dimension));

            this.wkt = GeometryType.LINESTRING.ToString() + " " + dimension.WktEncode() + "(";
            this.ewkt = "SRID=4326;" + this.Type.ToString() + " " + dimension.WktEncode() + "(";
            this.Validation = new LineString();

            int npoints = m_random.Next(2, Generator.MaxPoints);
            m_arr.AddBytes(intToBytes(npoints));

            for (int i = 0; i < npoints; i++)
            {
                addCoordinate(dimension, out Point pt, negative);
                (this.Validation as LineString).Points.Add(pt);

                if (i < npoints - 1)
                {
                    this.wkt += ",";
                    this.ewkt += ",";
                }
            }

            this.wkt += ")";
            this.ewkt += ")";
            this.wkb_big = m_arr.BigEndian.ToArray();
            this.wkb_little = m_arr.LittleEndian.ToArray();
        }
        public void CreateMultiLineString(DimensionType dimension, bool negative)
        {
            this.Type = GeometryType.MULTILINESTRING;
            this.Dimension = dimension;
            m_arr = new BinaryArray();
            m_arr.AddBytes(intToBytes((int)this.Type + (int)dimension));

            this.wkt = GeometryType.MULTILINESTRING.ToString() + " " + dimension.WktEncode() + "(";
            this.ewkt = "SRID=4326;" + this.Type.ToString() + " " + dimension.WktEncode() + "(";
            this.Validation = new MultiLineString();

            int nlines = m_random.Next(1, 5);
            m_arr.AddBytes(intToBytes(nlines));

            for (int i = 0; i < nlines; i++)
            {
                this.wkt += "(";
                this.ewkt += "(";
                LineString tmpls = new LineString();

                int npoints = m_random.Next(2, 4);
                m_arr.AddBytes(intToBytes(npoints));
                for (int j = 0; j < npoints; j++)
                {
                    addCoordinate(dimension, out Point pt, negative);
                    tmpls.AddGeometry(pt);

                    if (j < npoints - 1)
                    {
                        this.wkt += ",";
                        this.ewkt += ",";
                    }
                }

                (this.Validation as MultiLineString).AddGeometry(tmpls);

                if (i < npoints - 1)
                {
                    this.wkt += "),";
                    this.ewkt += "),";
                }
            }

            this.wkt += "))";
            this.ewkt += "))";
            this.wkb_big = m_arr.BigEndian.ToArray();
            this.wkb_little = m_arr.LittleEndian.ToArray();
        }
        //**************************************************************************************
        void addCoordinate(DimensionType dimension, out Point pt, bool negative)
        {
            double?[] values = new double?[] { null, null, null, null };

            for (int i = 0; i < dimension.GetDimensionValue(); i++)
            {
                double value = m_random.NextDouble();
                
                if (negative)
                    value = -value;

                values[i] = value;
                m_arr.AddBytes(doubleToBytes(value));
                this.wkt += value;
                this.ewkt += value;

                if (i < dimension.GetDimensionValue() - 1)
                {
                    this.wkt += " ";
                    this.ewkt += " ";
                }
            }

            //Set the validation element
            if (dimension == DimensionType.XYM)
            {
                pt = new Point(values[0], values[1], null, values[2]);
            }
            else
            {
                pt = new Point(values[0], values[1], values[2], values[3]);
            }
        }
        public override string ToString()
        {
            return wkt;
        }
        //**************************************************************************************
        byte endianToByte(EndianType value)
        {
            return Convert.ToByte(value);
        }
        byte[] intToBytes(int value)
        {
            if (BitConverter.IsLittleEndian)
                return BitConverter.GetBytes(value).Reverse().ToArray();
            else
                return BitConverter.GetBytes(value);
        }
        byte[] doubleToBytes(double value)
        {
            if (BitConverter.IsLittleEndian)
                return BitConverter.GetBytes(value).Reverse().ToArray();
            else
                return BitConverter.GetBytes(value);
        }
    }
}