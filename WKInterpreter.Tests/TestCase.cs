using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using WKInterpreter.CMD.TestData;
using Xunit.Abstractions;

namespace WKInterpreter.Tests
{
    public class TestCase : IXunitSerializable
    {
        public string Id { get; set; }
        public GeometryType Type { get; set; }
        public DimensionType Dimension { get; set; }
        public string wkt { get; set; }
        public string wkb { get; set; }
        public string ewkt { get; set; }
        public string ewkb { get; set; }
        public string wkbxdr { get; set; }
        public string ewkbxdr { get; set; }
        public string ewkbnosrid { get; set; }
        public string ewkbxdrnosrid { get; set; }
        public Geometry Validation { get; set; }

        public TestCase()
        {
        }

        public TestCase(JObject test)
        {

        }

        public TestCase(Test test)
        {
            Id = test.Id;
            Type = test.Type;
            Dimension = test.Dimension;
            wkt = test.wkt;
            wkb = test.wkb;
            ewkt = test.ewkt;
            ewkb = test.ewkb;
            wkbxdr = test.wkbxdr;
            ewkbxdr = test.ewkbxdr;
            ewkbnosrid = test.ewkbnosrid;
            ewkbxdrnosrid = test.ewkbxdrnosrid;
            Validation = test.Validation;
        }

        public void Deserialize(IXunitSerializationInfo info)
        {
            Id = info.GetValue<string>("Id");
            Type = info.GetValue<GeometryType>("Type");
            Dimension = info.GetValue<DimensionType>("Dimension");
            wkt = info.GetValue<string>("wkt");
            wkb = info.GetValue<string>("wkb");
            ewkt = info.GetValue<string>("ewkt");


        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue("Id", Id);
            info.AddValue("Dimension", Dimension);
            info.AddValue("Type", Type);
            info.AddValue("wkt", wkt);
            info.AddValue("wkb", wkb);
            info.AddValue("ewkt", ewkt);
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} - {2}",Id, Dimension, Type);
        }
    }
}
