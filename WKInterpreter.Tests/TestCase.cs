using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using WKInterpreter.Extensions;
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
        public byte[] wkb_big { get; set; }
        public byte[] wkb_little { get; set; }
        public string ewkt { get; set; }
        public byte[] ewkb_big { get; set; }
        public byte[] ewkb_little { get; set; }
        public Geometry Validation { get; set; }

        public TestCase()
        {
        }

        public TestCase(JToken test)
        {
            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                if (prop.Name == "Validation")
                    continue;

                prop.SetValue(this, test[prop.Name].ToObject(prop.PropertyType));
            }

            //Set the validation object
            Validation = test["Validation"].ToObject(this.Type.GetEquivalentType()) as Geometry;
        }

        public TestCase(Test test)
        {
            Id = test.Id;
            Type = test.Type;
            Dimension = test.Dimension;
            wkt = test.wkt;
            wkb_big = test.wkb_big;
            wkb_little = test.wkb_little;
            ewkt = test.ewkt;
            Validation = test.Validation;
        }

        public void Deserialize(IXunitSerializationInfo info)
        {
            Id = info.GetValue<string>("Id");
            Type = info.GetValue<GeometryType>("Type");
            Dimension = info.GetValue<DimensionType>("Dimension");
            wkt = info.GetValue<string>("wkt");
            wkb_big = info.GetValue<byte[]>("wkb_big");
            wkb_little = info.GetValue<byte[]>("wkb_little");
            ewkt = info.GetValue<string>("ewkt");
            ewkb_big = info.GetValue<byte[]>("ewkb_big");
            ewkb_little = info.GetValue<byte[]>("ewkb_little");
            Validation = JsonConvert.DeserializeObject(info.GetValue<string>("Validation"), Type.GetEquivalentType()) as Geometry;
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue("Id", Id);
            info.AddValue("Dimension", Dimension);
            info.AddValue("Type", Type);
            info.AddValue("wkt", wkt);
            info.AddValue("wkb_big", wkb_big);
            info.AddValue("wkb_little", wkb_little);
            info.AddValue("ewkt", ewkt);
            info.AddValue("ewkb_big", ewkb_big);
            info.AddValue("ewkb_little", ewkb_little);
            info.AddValue("Validation", JsonConvert.SerializeObject(Validation));
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} - {2}", Id, Dimension, Type);
        }
    }
}
