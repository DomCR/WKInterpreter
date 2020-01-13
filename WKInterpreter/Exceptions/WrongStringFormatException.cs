using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WKInterpreter.Exceptions
{
    [Serializable]
    public class WrongStringFormatException : Exception
    {
        public WrongStringFormatException() { }
        public WrongStringFormatException(string message) : base(message) { }
        public WrongStringFormatException(string message, Exception inner) : base(message, inner) { }
        protected WrongStringFormatException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
