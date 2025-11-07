
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
using System.Collections.Generic;
using UltimateDoomBuilder.Core.Geometry;

#endregion

namespace UltimateDoomBuilder.Core.Map
{
	/// <summary>
	/// Represents a Linedef (line definition) in a Doom map.
	/// A linedef connects two vertices and can have front and/or back sidedefs.
	/// </summary>
	public class Linedef : MapElement
	{
		#region ================== Constants

		public const int NUM_ARGS = 5;

		#endregion

		#region ================== Variables

		// Vertices
		private Vertex start;
		private Vertex end;

		// Sidedefs
		private Sidedef? front;
		private Sidedef? back;

		// Cache
		private bool updateneeded;
		private double lengthsq;
		private double length;
		private double angle;

		// Properties
		private Dictionary<string, bool> flags;
		private int action;
		private int activate;
		private List<int> tags;
		private int[] args;

		#endregion

		#region ================== Properties

		public Vertex Start { get { return start; } }
		public Vertex End { get { return end; } }
		public Sidedef? Front { get { return front; } }
		public Sidedef? Back { get { return back; } }
		public Line2D Line { get { return new Line2D(start.Position, end.Position); } }
		public Dictionary<string, bool> Flags { get { return flags; } }
		
		public int Action 
		{ 
			get { return action; } 
			set 
			{ 
				BeforeFieldsChange(); 
				action = value; 
			} 
		}
		
		public int Activate 
		{ 
			get { return activate; } 
			set 
			{ 
				BeforeFieldsChange(); 
				activate = value; 
			} 
		}

		public int Tag 
		{ 
			get { return tags[0]; } 
			set 
			{ 
				BeforeFieldsChange(); 
				tags[0] = value; 
			} 
		}
		
		public List<int> Tags 
		{ 
			get { return tags; } 
			set 
			{ 
				BeforeFieldsChange(); 
				tags = value; 
			} 
		}

		public double LengthSq { get { UpdateCache(); return lengthsq; } }
		public double Length { get { UpdateCache(); return length; } }
		public double Angle { get { UpdateCache(); return angle; } }
		public int AngleDeg { get { return (int)(Angle * Angle2D.PIDEG); } }
		public int[] Args { get { return args; } }

		#endregion

		#region ================== Constructor / Disposer

		/// <summary>
		/// Creates a new linedef connecting two vertices.
		/// </summary>
		public Linedef(Vertex start, Vertex end)
		{
			if (start == null) throw new ArgumentNullException(nameof(start));
			if (end == null) throw new ArgumentNullException(nameof(end));

			// Initialize
			this.elementtype = MapElementType.LINEDEF;
			this.start = start;
			this.end = end;
			this.updateneeded = true;
			this.args = new int[NUM_ARGS];
			this.tags = new List<int> { 0 };
			this.flags = new Dictionary<string, bool>(StringComparer.Ordinal);
		}

		#endregion

		#region ================== Methods

		/// <summary>
		/// Updates the cached geometry values.
		/// </summary>
		private void UpdateCache()
		{
			if (!updateneeded) return;

			Vector2D delta = end.Position - start.Position;
			lengthsq = delta.GetLengthSq();
			length = Math.Sqrt(lengthsq);
			angle = delta.GetAngle();
			updateneeded = false;
		}

		/// <summary>
		/// Attaches a front sidedef to this linedef.
		/// </summary>
		public void SetFront(Sidedef? sidedef)
		{
			BeforeFieldsChange();
			front = sidedef;
		}

		/// <summary>
		/// Attaches a back sidedef to this linedef.
		/// </summary>
		public void SetBack(Sidedef? sidedef)
		{
			BeforeFieldsChange();
			back = sidedef;
		}

		/// <summary>
		/// Sets a flag value.
		/// </summary>
		public void SetFlag(string flagname, bool value)
		{
			BeforeFieldsChange();
			if (value)
				flags[flagname] = true;
			else
				flags.Remove(flagname);
		}

		/// <summary>
		/// Gets a flag value.
		/// </summary>
		public bool GetFlag(string flagname)
		{
			return flags.ContainsKey(flagname) && flags[flagname];
		}

		/// <summary>
		/// Returns true if this linedef is single-sided (only front sidedef).
		/// </summary>
		public bool IsSingleSided()
		{
			return front != null && back == null;
		}

		/// <summary>
		/// Returns true if this linedef is double-sided (both front and back sidedefs).
		/// </summary>
		public bool IsDoubleSided()
		{
			return front != null && back != null;
		}

		/// <summary>
		/// Returns a string representation of this linedef.
		/// </summary>
		public override string ToString()
		{
			return $"Linedef from ({start.Position.x}, {start.Position.y}) to ({end.Position.x}, {end.Position.y})";
		}

		#endregion
	}
}
