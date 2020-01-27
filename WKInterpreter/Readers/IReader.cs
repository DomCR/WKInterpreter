using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WKInterpreter.Readers
{
    /// <summary>
    /// Interface for readers
    /// </summary>
    public interface IReader : IDisposable
    {
        /// <summary>
        /// Start reading the Stream of the reader.
        /// </summary>
        /// <returns></returns>
        Geometry Read();

        //Readers by type
        /// <summary>
        /// Reads the geometry of a point ([geometry])
        /// </summary>
        /// <returns></returns>
        Point ReadPoint(DimensionType dimension);
        LineString ReadLineString(DimensionType dimension);
        Polygon ReadPolygon(DimensionType dimension);
        MultiPoint ReadMultiPoint(DimensionType dimension);
        MultiLineString ReadMultiLineString(DimensionType dimension);
        MultiPolygon ReadMultiPolygon(DimensionType dimension);
    }
}
