
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

#endregion

namespace UltimateDoomBuilder.Core.Map
{
	/// <summary>
	/// Represents a Sector in a Doom map.
	/// </summary>
	public class Sector : MapElement
	{
		#region ================== Variables

		// Properties
		private int floorheight;
		private int ceilingheight;
		private string floortexture;
		private string ceilingtexture;
		private int effect;
		private int tag;
		private int brightness;

		#endregion

		#region ================== Properties

		public int FloorHeight 
		{ 
			get { return floorheight; } 
			set 
			{ 
				BeforeFieldsChange(); 
				floorheight = value; 
			} 
		}

		public int CeilingHeight 
		{ 
			get { return ceilingheight; } 
			set 
			{ 
				BeforeFieldsChange(); 
				ceilingheight = value; 
			} 
		}

		public string FloorTexture 
		{ 
			get { return floortexture; } 
			set 
			{ 
				BeforeFieldsChange(); 
				floortexture = value ?? "-"; 
			} 
		}

		public string CeilingTexture 
		{ 
			get { return ceilingtexture; } 
			set 
			{ 
				BeforeFieldsChange(); 
				ceilingtexture = value ?? "-"; 
			} 
		}

		public int Effect 
		{ 
			get { return effect; } 
			set 
			{ 
				BeforeFieldsChange(); 
				effect = value; 
			} 
		}

		public int Tag 
		{ 
			get { return tag; } 
			set 
			{ 
				BeforeFieldsChange(); 
				tag = value; 
			} 
		}

		public int Brightness 
		{ 
			get { return brightness; } 
			set 
			{ 
				BeforeFieldsChange(); 
				brightness = Math.Clamp(value, 0, 255); 
			} 
		}

		#endregion

		#region ================== Constructor / Disposer

		/// <summary>
		/// Creates a new Sector with default properties.
		/// </summary>
		public Sector()
		{
			// Initialize
			this.elementtype = MapElementType.SECTOR;
			this.floorheight = 0;
			this.ceilingheight = 128;
			this.floortexture = "-";
			this.ceilingtexture = "-";
			this.effect = 0;
			this.tag = 0;
			this.brightness = 160;
		}

		/// <summary>
		/// Creates a new Sector with specified heights.
		/// </summary>
		public Sector(int floorheight, int ceilingheight) : this()
		{
			this.floorheight = floorheight;
			this.ceilingheight = ceilingheight;
		}

		#endregion

		#region ================== Methods

		/// <summary>
		/// Returns a string representation of this sector.
		/// </summary>
		public override string ToString()
		{
			return $"Sector (floor: {floorheight}, ceiling: {ceilingheight}, tag: {tag})";
		}

		#endregion
	}
}
