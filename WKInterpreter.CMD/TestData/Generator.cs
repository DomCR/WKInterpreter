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

            foreach (GeometryType geometry in geometryTypes)
            {
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
        private static Test createEmpty(GeometryType geometry, DimensionType dimension)
        {
            Test test = new Test();
            test.Type = geometry;
            test.Dimension = dimension;

            test.wkt = geometry.ToString() + " " + dimension.WktEncode() + " EMPTY";
            test.ewkt = "SRID=4326;" + geometry.ToString() + " " + dimension.WktEncode() + " EMPTY";

            return test;
        }
        private static Test createTest(GeometryType geometry, DimensionType dimension)
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
        private static Test createPointTest(DimensionType dimension)
        {
            Test test = new Test();
            test.Type = GeometryType.POINT;
            test.Dimension = dimension;

            test.wkt = "POINT " + dimension.WktEncode() + "(";

            for (int i = 0; i < dimension.GetDimensionValue(); i++)
            {
                int value = m_random.Next();
                test.wkt += value;

                if (i < dimension.GetDimensionValue() - 1)
                    test.wkt += " ";
            }
            test.wkt += ")";

            test.Validation = new Point();

            return test;
        }
    }
}
