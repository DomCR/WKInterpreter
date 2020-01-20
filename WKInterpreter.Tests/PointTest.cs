using System;
using Xunit;

namespace WKInterpreter.Tests
{
    public class PointTest
    {
        [Fact]
        public void TestDefaultContructors()
        {
            Point pt = new Point();

            Assert.True(pt.IsEmpty);
            Assert.True(pt.Dimension == DimensionType.XY);
            Assert.True(pt.X == null);
            Assert.True(pt.Y == null);
            Assert.True(pt.Z == null);
            Assert.True(pt.M == null);
        }
        [Fact]
        public void TestDeserializeEmpy()
        {
            //Empty test
            Point pt = Geometry.Deserialize("POINT EMPTY") as Point;
            Assert.True(pt.IsEmpty);

        }
        [Fact]
        public void TestDeserializeXY()
        {
            Point pt = Geometry.Deserialize("POINT(1 2)") as Point;

            Assert.True(pt.X == 1);
            Assert.True(pt.Y == 2);
            Assert.True(pt.Z == null);
            Assert.True(pt.M == null);
        }

        [Fact]
        public void TestDeserializeXYZ()
        {
            Point pt = Geometry.Deserialize("POINT Z(1 2 3)") as Point;

            Assert.True(pt.X == 1);
            Assert.True(pt.Y == 2);
            Assert.True(pt.Z == 3);
            Assert.True(pt.M == null);
        }

        [Fact]
        public void TestDeserializeXYM()
        {
            Point pt = Geometry.Deserialize("POINT M(1 2 3)") as Point;

            Assert.True(pt.X == 1);
            Assert.True(pt.Y == 2);
            Assert.True(pt.Z == null);
            Assert.True(pt.M == 3);
        }
    }
}
