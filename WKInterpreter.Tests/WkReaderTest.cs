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
    public class WkReaderTest
    {
        public static TheoryData<TestCase> TestData;

        static WkReaderTest()
        {
            TestData = new TheoryData<TestCase>();
            string path = "../../../test_model.json";
            JObject model = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(path));

            foreach (KeyValuePair<string, JToken> cases in model)
            {
                JEnumerable<JToken> tests = cases.Value.Children();
                foreach (JToken test in tests)
                {
                    TestCase tCase = new TestCase(test);
                    TestData.Add(tCase);
                }
            }
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void ParseWkb_big(TestCase testCase)
        {
            Geometry result = Geometry.Deserialize(testCase.wkb_big);

            Assert.True(result.IsValid);
            Assert.True(result.Equals(testCase.Validation));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void ParseWkb_little(TestCase testCase)
        {
            Geometry result = Geometry.Deserialize(testCase.wkb_little);

            Assert.True(result.IsValid);
            Assert.True(result.Equals(testCase.Validation));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void ParseWkt(TestCase testCase)
        {
            Geometry result = Geometry.Deserialize(testCase.wkt);

            Assert.True(result.IsValid, "IsValid failed, test id: " + testCase.Id);
            Assert.True(result.Equals(testCase.Validation), "Validation failed, test id: " + testCase.Id);
        }
    }
}