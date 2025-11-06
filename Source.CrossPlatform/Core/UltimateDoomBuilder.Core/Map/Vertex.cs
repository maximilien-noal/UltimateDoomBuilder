
#region ================== Copyright (c) 2007 Pascal vd Heiden

/*
 * Copyright (c) 2007 Pascal vd Heiden, www.codeimp.com
 * This program is released under GNU General Public License
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 */

#endregion

#region ================== Namespaces

using System;
using UltimateDoomBuilder.Core.Geometry;

#endregion

namespace UltimateDoomBuilder.Core.Map
{
	/// <summary>
	/// Represents a vertex in a Doom map.
	/// </summary>
	public class Vertex : MapElement
	{
		#region ================== Variables

		// Position
		private Vector2D pos;

		// Height information
		private double zfloor;
		private double zceiling;
		
		#endregion

		#region ================== Properties

		public Vector2D Position { get { return pos; } }
		
		public double ZCeiling 
		{
			get { return zceiling; }
			set 
			{
				if(zceiling != value) 
				{
					BeforeFieldsChange();
					zceiling = value;
				}
			}
		}
		
		public double ZFloor 
		{
			get { return zfloor; }
			set 
			{
				if(zfloor != value) 
				{
					BeforeFieldsChange();
					zfloor = value;
				}
			}
		}

		#endregion

		#region ================== Constructor / Disposer

		/// <summary>
		/// Creates a new vertex at the specified position.
		/// </summary>
		public Vertex(Vector2D pos)
		{
			// Check coordinates
			if (double.IsNaN(pos.x) || double.IsNaN(pos.y))
			{
				throw new ArgumentException("Vertex position cannot have NaN coordinates");
			}

			// Initialize
			this.elementtype = MapElementType.VERTEX;
			this.pos = pos;
			this.zfloor = double.NaN;
			this.zceiling = double.NaN;
		}

		/// <summary>
		/// Creates a new vertex at the specified coordinates.
		/// </summary>
		public Vertex(double x, double y) : this(new Vector2D(x, y))
		{
		}

		#endregion

		#region ================== Methods

		/// <summary>
		/// Moves the vertex to a new position.
		/// </summary>
		public void Move(Vector2D newpos)
		{
			if (double.IsNaN(newpos.x) || double.IsNaN(newpos.y))
			{
				throw new ArgumentException("Vertex position cannot have NaN coordinates");
			}

			BeforeFieldsChange();
			this.pos = newpos;
		}

		/// <summary>
		/// Moves the vertex to new coordinates.
		/// </summary>
		public void Move(double x, double y)
		{
			Move(new Vector2D(x, y));
		}

		/// <summary>
		/// Returns a string representation of this vertex.
		/// </summary>
		public override string ToString()
		{
			return $"Vertex at ({pos.x}, {pos.y})";
		}

		#endregion
	}
}
