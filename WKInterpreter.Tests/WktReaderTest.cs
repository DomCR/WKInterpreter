using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WKInterpreter.CMD.TestData;
using Xunit;

namespace WKInterpreter.Tests
{
    public class WktReaderTest
    {
        public static TheoryData<TestCase> TestData;

        static WktReaderTest()
        {
            TestData = new TheoryData<TestCase>();
            string path = "../../../test_model.json";
            JObject model = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(path));
            //TestModel model = JsonConvert.DeserializeObject<TestModel>(File.ReadAllText(path));

            foreach (KeyValuePair<string, JToken> cases in model)
            {
                JEnumerable<JToken> tests = cases.Value.Children();
                foreach (var test in tests)
                {

                }
                //if (test.Type == GeometryType.POINT)
                TestData.Add(new TestCase());
            }
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void ParseWkt(TestCase testCase)
        {
            Geometry result = Geometry.Deserialize(testCase.wkt);

            Assert.True(result.IsValid);
        }
    }
}
