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
        public static int MaxPoints = 20;
        private static readonly Random m_random = new Random(Seed);

        public static TestModel CreateTestModel()
        {
            TestModel model = new TestModel();
            //Create different test foreach type of geometry
            GeometryType[] geometryTypes = Enum.GetValues(typeof(GeometryType)).Cast<GeometryType>().ToArray();
            DimensionType[] dimensions = Enum.GetValues(typeof(DimensionType)).Cast<DimensionType>().ToArray();


            geometryTypes = new GeometryType[] { GeometryType.POINT, GeometryType.LINESTRING };
            foreach (GeometryType geometry in geometryTypes)
            {
                foreach (DimensionType dimension in dimensions)
                {
                    Test empty = new Test();
                    empty.CreateEmpty(geometry, dimension);
                    model.AddTest(empty);

                    Test basic = createTest(geometry, dimension, false);
                    model.AddTest(basic);

                    Test negative = createTest(geometry, dimension, true);
                    model.AddTest(negative);

                    //Write the added tests
                    if (basic != null && negative != null)
                    {
                        Console.WriteLine(basic.ToString());
                        Console.WriteLine(negative.ToString());
                    }
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

            test.wkb_big = arr.BigEndian.ToArray();
            test.wkb_little = arr.LittleEndian.ToArray();

            test.Validation = Activator.CreateInstance(geometry.GetEquivalentType()) as Geometry;
            return test;
        }
        static Test createTest(GeometryType geometry, DimensionType dimension, bool isNegative)
        {
            Test test = new Test();

            switch (geometry)
            {
                case GeometryType.GEOMETRY:
                    break;
                case GeometryType.POINT:
                    test.CreatePointTest(dimension, isNegative);
                    break;
                case GeometryType.LINESTRING:
                    test.CreateLineStringTest(dimension, isNegative);
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

            return test;
        }
        //**************************************************************************************
        static void createCoordinate(DimensionType dimension, ref BinaryArray arr, ref string test, out Point pt)
        {
            double?[] values = new double?[] { null, null, null, null };

            for (int i = 0; i < dimension.GetDimensionValue(); i++)
            {
                double value = m_random.NextDouble();
                values[i] = value;
                arr.AddBytes(doubleToBytes(value));
                test += value;

                if (i < dimension.GetDimensionValue() - 1)
                    test += " ";
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
        static Test createLineStringTest(DimensionType dimension)
        {
            Test test = new Test();
            test.Type = GeometryType.LINESTRING;
            test.Dimension = dimension;

            //Array to store the byte form
            BinaryArray arr = new BinaryArray();
            arr.AddBytes(intToBytes((int)GeometryType.LINESTRING + (int)dimension));

            int npoints = m_random.Next(2, MaxPoints);

            //**********************************************************
            #region Set wkt case
            test.wkt = test.Type + " " + dimension.WktEncode() + "(";

            for (int j = 0; j < npoints; j++)
            {
                for (int i = 0; i < dimension.GetDimensionValue(); i++)
                {
                    double value = m_random.NextDouble();
                    //values[i] = value;
                    arr.AddBytes(doubleToBytes(value));
                    test.wkt += value;

                    if (i < dimension.GetDimensionValue() - 1)
                        test.wkt += " ";
                }

                if (j < npoints - 1)
                    test.wkt += ",";
            }

            test.wkt += ")";
            #endregion
            //**********************************************************
            test.wkb_big = arr.BigEndian.ToArray();
            test.wkb_little = arr.LittleEndian.ToArray();
            //**********************************************************

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
