using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WKInterpreter.Tests
{
    public class LineStringTest
    {
        [Fact]
        public void TestDefaultConstructor()
        {
            LineString line = new LineString();

            Assert.True(line.IsEmpty);
            Assert.True(line.Points.Count == 0);
            Assert.True(line.Dimension == DimensionType.XY);
        }
    }
}
