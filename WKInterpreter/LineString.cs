using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WKInterpreter.Exceptions;

namespace WKInterpreter
{
	/// <summary>
	/// LineString class, implements a geometry collection of points.
	/// </summary>
	public class LineString : GeometryCollection<Point>, IEquatable<LineString>
	{
		/// <summary>
		/// List of points inside the line.
		/// </summary>
		public List<Point> Points { get { return m_geometries; } }
		/// <summary>
		/// Geometry type of the object, LINESTRING.
		/// </summary>
		public override GeometryType GeometryType { get { return GeometryType.LINESTRING; } }
		/// <summary>
		/// LineString validation.
		/// </summary>
		public override bool IsValid
		{
			get
			{
				if (!base.IsValid)
					return false;

				//Line must have min 2 points
				if (m_geometries.Count == 1)
					return false;

				return true;
			}
		}
		//*********************************************************************************
		public LineString() : base() { }
		public LineString(IEnumerable<Point> geometries) : base(geometries) { }
		//*********************************************************************************
		public void ChangeDimension(DimensionType dimension)
		{
			if (this.IsEmpty)
				throw new NotSupportedException("The LineString cannot be empty to change the dimension.");

			//Change the dimension of all the points int the element
			//Should put a default value?

			throw new NotImplementedException();
		}
		/// <summary>
		/// Check if the line is closed.
		/// </summary>
		/// <returns></returns>
		public bool IsClosed()
		{
			if (m_geometries.Count == 0)
				throw new Exception();

			if (m_geometries.Count == 1)
				return false;

			return m_geometries.FirstOrDefault().IsNear(m_geometries.Last());
		}
		/// <summary>
		/// Determines whether the specified object is equal to the current LineString.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public override bool Equals(object other)
		{
			if (other == null)
				return false;
			if (!(other is LineString))
				return false;

			return Equals((LineString)other);
		}
		/// <summary>
		/// Determines whether the specified LineString is equal to the current LineString.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(LineString other)
		{
			return Points.SequenceEqual(other.Points);
		}
		/// <summary>
		/// Generate the hash code for this object.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return new { Points }.GetHashCode();
		}
	}
}
