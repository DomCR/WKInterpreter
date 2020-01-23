using Newtonsoft.Json;
using System;
using System.IO;
using WKInterpreter.CMD.TestData;

namespace WKInterpreter.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            TestModel model = Generator.CreateTestModel();

            using (StreamWriter sw = new StreamWriter("../../../../WKInterpreter.Tests/test_model.json"))
            {
                sw.Write(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
            //****************************************************************
            Console.WriteLine("WKInterpreter.CMD execution ended.");
            Console.ReadLine();
        }
    }
}
