using System;
using System.Collections.Generic;
using System.Text;

namespace WKInterpreter.Extensions
{
    public static class GeometryTypeExtensions
    {
        public static Type GetEquivalentType(this GeometryType geometry)
        {
            switch (geometry)
            {
                case GeometryType.GEOMETRY:
                    return typeof(Geometry);
                case GeometryType.POINT:
                    return typeof(Point);
                case GeometryType.LINESTRING:
                    return typeof(LineString);
                case GeometryType.POLYGON:
                    return typeof(Polygon);
                case GeometryType.MULTIPOINT:
                    return typeof(MultiPoint);
                case GeometryType.MULTILINESTRING:
                    return typeof(MultiLineString);
                case GeometryType.MULTIPOLYGON:
                    return typeof(MultiPolygon);
                case GeometryType.GEOMETRYCOLLECTION:
                    return typeof(GeometryCollection<Geometry>);
                case GeometryType.CIRCULARSTRING:
                    //return typeof(CircularString);
                case GeometryType.COMPOUNDCURVE:

                case GeometryType.CURVEPOLYGON:

                case GeometryType.MULTICURVE:

                case GeometryType.MULTISURFACE:

                case GeometryType.CURVE:

                case GeometryType.SURFACE:

                case GeometryType.POLYHEDRALSURFACE:

                case GeometryType.TIN:

                case GeometryType.TRIANGLE:

                case GeometryType.CIRCLE:

                case GeometryType.GEODESICSTRING:
               
                case GeometryType.ELLIPTICALCURVE:
                  
                case GeometryType.URBSCURVE:
               
                case GeometryType.CLOTHOID:
         
                case GeometryType.SPIRALCURVE:
                    
                case GeometryType.COMPOUNDSURFACE:
                   
                case GeometryType.BREPSOLID:
                    
                case GeometryType.AFFINEPLACEMENT:
                    
                default:
                    //return null;
                    return typeof(Geometry);
            }
        }
    }
}
