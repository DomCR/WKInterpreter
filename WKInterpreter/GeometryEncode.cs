namespace WKInterpreter
{
    /// <summary>
    /// Geometry encode relation.
    /// </summary>
    public enum GeometryEncode
    {
        /// <summary>
        /// Not defined geometry.
        /// </summary>
        GEOMETRY_2D = 0x0000,
        GEOMETRY_Z = 0x1000,
        GEOMETRY_M = 0x2000,
        GEOMETRY_ZM = 0x3000,


        /// <summary>
        /// 2D Point.
        /// </summary>
        POINT_2D = 0x0001,
        /// <summary>
        /// 3D Point.
        /// </summary>
        POINT_Z = 0x1001,
        /// <summary>
        /// 3D Point using componen M instead of Z.
        /// </summary>
        POINT_M = 0x2001,
        /// <summary>
        /// 4D Point.
        /// </summary>
        POINT_ZM = 0x3001,

        LINESTRING = 0x0002,
        POLYGON = 0x0003,
        MULTIPOINT = 0x0004,
        MULTILINESTRING = 0x0005,
        MULTIPOLYGON = 0x0006,
        GEOMETRYCOLLECTION = 0x0007,
        CIRCULARSTRING = 0x0008,
        COMPOUNDCURVE = 0x0009,
        CURVEPOLYGON = 0x0010,
        MULTICURVE = 0x0011,
        MULTISURFACE = 0x0012,
        CURVE = 0x0013,
        SURFACE = 0x0014,
        POLYHEDRALSURFACE = 0x0015,
        TIN = 0x0016,
        TRIANGLE = 0x0017,
        CIRCLE = 0x0018,
        GEODESICSTRING = 0x0019,
        ELLIPTICALCURVE = 0x0020,
        URBSCURVE = 0x0021,
        CLOTHOID = 0x0022,
        SPIRALCURVE = 0x0023,
        COMPOUNDSURFACE = 0x0024,
        BREPSOLID = 0x0025, //don't have 2D code
        AFFINEPLACEMENT = 0x0026, //don't have 2D code
    }
}
