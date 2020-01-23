# WKInterpreter
Well-known text-binary representation of coordinate reference systems interpreter for C#.

### Supported geometries

Based on the Wikipedia page : https://en.wikipedia.org/wiki/Well-known_text_representation_of_geometry

| Geometry | WKT | WKB | Passing |
| -------- | --- | --- | ------- | 
|Point|True|True|False|
|LineString|False|False|False|
|Polygon|False|False|False|
|MultiPoint|False|False|False|
|MultiLineString|False|False|False|
|MultiPolygon|False|False|False|
|GeometryCollection|False|False|False|
|CircularString|False|False|False|
|CompoundCurve|False|False|False|
|CurvePolygon|False|False|False|
|MultiCurve|False|False|False|
|MultiSurface|False|False|False|
|Curve|False|False|False|
|Surface|False|False|False|
|PolyhedralSurface|False|False|False|
|TIN|False|False|False|
|Triangle|False|False|False|
|Circle|False|False|False|
|GeodesicString|False|False|False|
|EllipticalCurve|False|False|False|
|NurbsCurve|False|False|False|
|Clothoid|False|False|False|
|SpiralCurve|False|False|False|
|CompoundSurface|False|False|False|
|BrepSolid|False|False|False|
|AffinePlacement|False|False|False|