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

            byte[] blop = new byte[] 
            {
                0x01,
                0x01, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xf0, 0x3f, 
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40 
            };

            pt = Geometry.Deserialize(blop) as Point;

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

            byte[] blop = new byte[]
            {
                0x01,
                0xe9, 0x03, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xf0, 0x3f,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x40
            };

            pt = Geometry.Deserialize(blop) as Point;

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
