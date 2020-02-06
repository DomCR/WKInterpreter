using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WKInterpreter.Exceptions;

namespace WKInterpreter
{
    public class Polygon : Surface, IWKSerializable
    {
        public List<Surface> InnerShapes { get;  }
        public override GeometryType GeometryType { get { return GeometryType.POLYGON; } }
        public override bool IsValid
        {
            get
            {
                //Base check
                if (!base.IsValid)
                    return false;

                return true;
            }
        }
        public Polygon() : base()
        {
            InnerShapes = new List<Surface>();
        }
        public void AddInnerShape(Surface shape)
        {
            if (!this.IsEmpty && shape.Dimension != Dimension)
                throw new InvalidDimensionException();
            if (shape.IsEmpty)
                throw new ArgumentException("Cannot add an empty element.");

            InnerShapes.Add(shape);
        }
    }
}
