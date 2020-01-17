using System;
using System.Collections.Generic;
using System.Text;

namespace WKInterpreter.Readers
{
    public interface IReader : IDisposable
    {
        Geometry Read();
    }
}
