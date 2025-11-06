
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
	/// Represents a Sidedef (side of a linedef) in a Doom map.
	/// A sidedef connects a linedef to a sector and defines wall textures.
	/// </summary>
	public class Sidedef : MapElement
	{
		#region ================== Variables

		// Owner
		private Linedef? linedef;

		// Sector
		private Sector? sector;

		// Properties
		private int offsetx;
		private int offsety;
		private string texnamehigh;
		private string texnamemid;
		private string texnamelow;

		// UDMF properties
		private Dictionary<string, bool> flags;

		#endregion

		#region ================== Properties

		public bool IsFront { get { return (linedef != null) && (this == linedef.Front); } }
		public Linedef? Line { get { return linedef; } }
		public Sidedef? Other 
		{ 
			get 
			{ 
				if (linedef == null) return null;
				return (this == linedef.Front ? linedef.Back : linedef.Front); 
			} 
		}
		
		public Sector? Sector { get { return sector; } }
		public Dictionary<string, bool> Flags { get { return flags; } }
		
		public double Angle 
		{ 
			get 
			{ 
				if (linedef == null) return 0;
				return (IsFront ? linedef.Angle : Angle2D.Normalized(linedef.Angle + Angle2D.PI)); 
			} 
		}

		public int OffsetX 
		{ 
			get { return offsetx; } 
			set 
			{ 
				BeforeFieldsChange(); 
				offsetx = value; 
			} 
		}

		public int OffsetY 
		{ 
			get { return offsety; } 
			set 
			{ 
				BeforeFieldsChange(); 
				offsety = value; 
			} 
		}

		public string HighTexture { get { return texnamehigh; } }
		public string MiddleTexture { get { return texnamemid; } }
		public string LowTexture { get { return texnamelow; } }

		#endregion

		#region ================== Constructor / Disposer

		/// <summary>
		/// Creates a new sidedef.
		/// </summary>
		public Sidedef(Sector? sector)
		{
			// Initialize
			this.elementtype = MapElementType.SIDEDEF;
			this.sector = sector;
			this.texnamehigh = "-";
			this.texnamemid = "-";
			this.texnamelow = "-";
			this.flags = new Dictionary<string, bool>(StringComparer.Ordinal);
			this.offsetx = 0;
			this.offsety = 0;
		}

		#endregion

		#region ================== Methods

		/// <summary>
		/// Attaches this sidedef to a linedef.
		/// </summary>
		public void SetLinedef(Linedef? line)
		{
			BeforeFieldsChange();
			this.linedef = line;
		}

		/// <summary>
		/// Sets the sector for this sidedef.
		/// </summary>
		public void SetSector(Sector? newsector)
		{
			BeforeFieldsChange();
			this.sector = newsector;
		}

		/// <summary>
		/// Sets the high (upper) texture.
		/// </summary>
		public void SetTextureHigh(string texturename)
		{
			BeforeFieldsChange();
			this.texnamehigh = texturename ?? "-";
		}

		/// <summary>
		/// Sets the middle texture.
		/// </summary>
		public void SetTextureMid(string texturename)
		{
			BeforeFieldsChange();
			this.texnamemid = texturename ?? "-";
		}

		/// <summary>
		/// Sets the low (lower) texture.
		/// </summary>
		public void SetTextureLow(string texturename)
		{
			BeforeFieldsChange();
			this.texnamelow = texturename ?? "-";
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
		/// Returns a string representation of this sidedef.
		/// </summary>
		public override string ToString()
		{
			string side = IsFront ? "front" : "back";
			return $"Sidedef ({side}) in sector {sector?.Index ?? -1}";
		}

		#endregion
	}
}
