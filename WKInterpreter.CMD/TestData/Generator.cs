using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WKInterpreter.Extensions;

namespace WKInterpreter.CMD.TestData
{
    public class Generator
    {
        public static int Seed = 0;
        private static readonly Random m_random = new Random(Seed);

        public static TestModel CreateTestModel()
        {
            TestModel model = new TestModel();
            //Create different test foreach type of geometry
            GeometryType[] geometryTypes = Enum.GetValues(typeof(GeometryType)).Cast<GeometryType>().ToArray();
            DimensionType[] dimensions = Enum.GetValues(typeof(DimensionType)).Cast<DimensionType>().ToArray();

            //foreach (GeometryType geometry in geometryTypes)
            {
                GeometryType geometry = GeometryType.POINT;
                foreach (DimensionType dimension in dimensions)
                {
                    Test empty = createEmpty(geometry, dimension);
                    model.AddTest(empty);
                    Test basic = createTest(geometry, dimension);
                    model.AddTest(basic);

                    //Write the added tests
                    if (basic != null)
                        Console.WriteLine(basic.ToString());
                }
            }

            return model;
        }
        static Test createEmpty(GeometryType geometry, DimensionType dimension)
        {
            Test test = new Test();
            test.Type = geometry;
            test.Dimension = dimension;

            //Array to store the byte form
            BinaryArray arr = new BinaryArray();
            arr.AddBytes(intToBytes((int)geometry + (int)dimension));

            test.wkt = geometry.ToString() + " " + dimension.WktEncode() + " EMPTY";
            test.ewkt = "SRID=4326;" + geometry.ToString() + " " + dimension.WktEncode() + " EMPTY";

            test.Validation = Activator.CreateInstance(geometry.GetEquivalentType()) as Geometry;
            return test;
        }
        static Test createTest(GeometryType geometry, DimensionType dimension)
        {
            switch (geometry)
            {
                case GeometryType.GEOMETRY:
                    break;
                case GeometryType.POINT:
                    return createPointTest(dimension);
                case GeometryType.LINESTRING:
                    break;
                case GeometryType.POLYGON:
                    break;
                case GeometryType.MULTIPOINT:
                    break;
                case GeometryType.MULTILINESTRING:
                    break;
                case GeometryType.MULTIPOLYGON:
                    break;
                case GeometryType.GEOMETRYCOLLECTION:
                    break;
                case GeometryType.CIRCULARSTRING:
                    break;
                case GeometryType.COMPOUNDCURVE:
                    break;
                case GeometryType.CURVEPOLYGON:
                    break;
                case GeometryType.MULTICURVE:
                    break;
                case GeometryType.MULTISURFACE:
                    break;
                case GeometryType.CURVE:
                    break;
                case GeometryType.SURFACE:
                    break;
                case GeometryType.POLYHEDRALSURFACE:
                    break;
                case GeometryType.TIN:
                    break;
                case GeometryType.TRIANGLE:
                    break;
                case GeometryType.CIRCLE:
                    break;
                case GeometryType.GEODESICSTRING:
                    break;
                case GeometryType.ELLIPTICALCURVE:
                    break;
                case GeometryType.URBSCURVE:
                    break;
                case GeometryType.CLOTHOID:
                    break;
                case GeometryType.SPIRALCURVE:
                    break;
                case GeometryType.COMPOUNDSURFACE:
                    break;
                case GeometryType.BREPSOLID:
                    break;
                case GeometryType.AFFINEPLACEMENT:
                    break;
                default:
                    break;
            }

            return null;
        }
        //**************************************************************************************
        static Test createPointTest(DimensionType dimension)
        {
            double?[] values = new double?[] { null, null, null, null };
            Test test = new Test();
            test.Type = GeometryType.POINT;
            test.Dimension = dimension;

            //Array to store the byte form
            BinaryArray arr = new BinaryArray();
            //arr.BigEndian.Add(endianToByte(EndianType.BIG_ENDIAN));
            //arr.LittleEndian.Add(endianToByte(EndianType.LITTLE_ENDIAN));
            arr.AddBytes(intToBytes((int)GeometryType.POINT + (int)dimension));          
            //**********************************************************
            #region Set wkt case
            test.wkt = "POINT " + dimension.WktEncode() + "(";
            for (int i = 0; i < dimension.GetDimensionValue(); i++)
            {
                double value = m_random.NextDouble();
                values[i] = value;
                arr.AddBytes(doubleToBytes(value));
                test.wkt += value;

                if (i < dimension.GetDimensionValue() - 1)
                    test.wkt += " ";
            }
            test.wkt += ")";
            #endregion
            //**********************************************************
            test.wkb_big = arr.BigEndian.ToArray();
            test.wkb_little = arr.LittleEndian.ToArray();
            //**********************************************************
            //Set the validation element
            if (dimension == DimensionType.XYM)
            {
                test.Validation = new Point(values[0], values[1], null, values[2]);
            }
            else
            {
                test.Validation = new Point(values[0], values[1], values[2], values[3]);
            }

            return test;
        }
        //**************************************************************************************
        static byte endianToByte(EndianType value)
        {
            return Convert.ToByte(value);
        }
        static byte[] doubleToBytes(double value) 
        {
            if (BitConverter.IsLittleEndian)
                return BitConverter.GetBytes(value).Reverse().ToArray();
            else
                return BitConverter.GetBytes(value);
        }
        static byte[] intToBytes(int value)
        {
            if (BitConverter.IsLittleEndian)
                return BitConverter.GetBytes(value).Reverse().ToArray();
            else
                return BitConverter.GetBytes(value);
        }
    }
}
