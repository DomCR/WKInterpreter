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
        public static int Maxlines = 20;

        public static TestModel CreateTestModel()
        {
            TestModel model = new TestModel();
            //Create different test foreach type of geometry
            GeometryType[] geometryTypes = Enum.GetValues(typeof(GeometryType)).Cast<GeometryType>().ToArray();
            DimensionType[] dimensions = Enum.GetValues(typeof(DimensionType)).Cast<DimensionType>().ToArray();

            geometryTypes = new GeometryType[] 
            {
                GeometryType.POINT, 
                GeometryType.LINESTRING ,
                GeometryType.MULTILINESTRING
            };

            foreach (GeometryType geometry in geometryTypes)
            {
                foreach (DimensionType dimension in dimensions)
                {
                    Test empty = new Test();
                    empty.CreateEmpty(geometry, dimension);
                    Test basic = createTest(geometry, dimension, false);
                    Test negative = createTest(geometry, dimension, true);

                    model.AddTest(empty);
                    model.AddTest(basic);
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
                    test.CreateMultiLineString(dimension, isNegative);
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
    }
}
