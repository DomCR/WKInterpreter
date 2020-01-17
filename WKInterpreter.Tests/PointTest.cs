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
    }
}
