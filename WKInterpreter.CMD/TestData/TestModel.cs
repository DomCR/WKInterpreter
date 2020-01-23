using System;
using System.Collections.Generic;
using System.Text;

namespace WKInterpreter.CMD.TestData
{
    public class TestModel
    {
        public List<Test> Cases { get; private set; }

        public TestModel()
        {
            Cases = new List<Test>();
        }
        public void AddTest(Test test)
        {
            if (test == null)
                return;

            test.Id = Cases.Count.ToString("0000");
            Cases.Add(test);
        }
    }
}
