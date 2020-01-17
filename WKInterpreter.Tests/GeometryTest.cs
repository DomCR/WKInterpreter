using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WKInterpreter.Tests
{
    public class GeometryTest
    {
        [Fact]
        public void DeserializationTest()
        {
            Geometry.Deserialize("POINT(1 2)");
        }
    }
}
