using System;

namespace WKInterpreter
{
    /// <summary>
    /// Well Known format, Geometry type.
    /// </summary>
    public enum GeometryType
    {
        /// <summary>
        /// Not defined geometry.
        /// </summary>
        GEOMETRY           = 0x00,
        /// <summary>
        /// Point.
        /// </summary>
        POINT              = 0x01,
        /// <summary>
        /// Polyline.
        /// </summary>
        LINESTRING         = 0x02,
        /// <summary>
        /// Polygon.
        /// </summary>
        POLYGON            = 0x03,
        /// <summary>
        /// Collection of points.
        /// </summary>
        MULTIPOINT         = 0x04,
        /// <summary>
        /// Collection fo polylines.
        /// </summary>
        MULTILINESTRING    = 0x05,
        /// <summary>
        /// Collection of polygons.
        /// </summary>
        MULTIPOLYGON       = 0x06,
        /// <summary>
        /// Collection of geometric objects.
        /// </summary>
        GEOMETRYCOLLECTION = 0x07,
        CIRCULARSTRING     = 0x08,
        COMPOUNDCURVE      = 0x09,
        CURVEPOLYGON       = 0x0A,
        MULTICURVE         = 0x0B,
        MULTISURFACE       = 0x0C,
        CURVE              = 0x0D,
        SURFACE            = 0x0E,
        POLYHEDRALSURFACE  = 0x0F,
        TIN                = 0x10,
        TRIANGLE           = 0x11,
        CIRCLE             = 0x12,
        GEODESICSTRING     = 0x13,
        ELLIPTICALCURVE    = 0x14,
        URBSCURVE          = 0x15,
        CLOTHOID           = 0x16,
        SPIRALCURVE        = 0x17,
        COMPOUNDSURFACE    = 0x18,
        /// <summary>
        /// Brep solid, only compatible with XYZ dimension.
        /// </summary>
        BREPSOLID          = 0x19, //don't have 2D code
        /// <summary>
        /// Affine Placement, only compatible with XY and XYZ dimensions.
        /// </summary>
        AFFINEPLACEMENT    = 0x66, //int value 102
    }
}
