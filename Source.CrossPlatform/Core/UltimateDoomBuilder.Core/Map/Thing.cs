
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
	/// Represents a Thing (entity/object) in a Doom map.
	/// </summary>
	public class Thing : MapElement
	{
		#region ================== Constants

		public const int NUM_ARGS = 5;

		#endregion

		#region ================== Variables

		// Properties
		private int type;
		private Vector3D pos;
		private int angledoom;      // Angle as entered / stored in file
		private double anglerad;    // Angle in radians
		private Dictionary<string, bool> flags;
		private int tag;
		private int action;
		private int[] args;
		private double scaleX;
		private double scaleY;
		private int pitch;
		private int roll;
		private double pitchrad;
		private double rollrad;

		#endregion

		#region ================== Properties

		public int Type 
		{ 
			get { return type; } 
			set 
			{ 
				BeforeFieldsChange(); 
				type = value; 
			} 
		}

		public Vector3D Position { get { return pos; } }
		public double ScaleX { get { return scaleX; } }
		public double ScaleY { get { return scaleY; } }
		public int Pitch { get { return pitch; } }
		public double PitchRad { get { return pitchrad; } }
		public int Roll { get { return roll; } }
		public double RollRad { get { return rollrad; } }
		public double Angle { get { return anglerad; } }
		public int AngleDoom { get { return angledoom; } }
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
		public int[] Args { get { return args; } }
		
		public int Tag 
		{ 
			get { return tag; } 
			set 
			{ 
				BeforeFieldsChange(); 
				tag = value; 
			} 
		}

		#endregion

		#region ================== Constructor / Disposer

		/// <summary>
		/// Creates a new Thing at the specified position.
		/// </summary>
		public Thing(Vector3D position, int type)
		{
			// Initialize
			this.elementtype = MapElementType.THING;
			this.pos = position;
			this.type = type;
			this.flags = new Dictionary<string, bool>(StringComparer.Ordinal);
			this.args = new int[NUM_ARGS];
			this.scaleX = 1.0;
			this.scaleY = 1.0;
			this.angledoom = 0;
			this.anglerad = 0.0;
		}

		/// <summary>
		/// Creates a new Thing at the specified coordinates.
		/// </summary>
		public Thing(double x, double y, double z, int type) 
			: this(new Vector3D(x, y, z), type)
		{
		}

		#endregion

		#region ================== Methods

		/// <summary>
		/// Moves the thing to a new position.
		/// </summary>
		public void Move(Vector3D newpos)
		{
			BeforeFieldsChange();
			this.pos = newpos;
		}

		/// <summary>
		/// Moves the thing to new coordinates.
		/// </summary>
		public void Move(double x, double y, double z)
		{
			Move(new Vector3D(x, y, z));
		}

		/// <summary>
		/// Sets the angle in degrees (Doom format: 0-359).
		/// </summary>
		public void SetAngle(int angledoom)
		{
			BeforeFieldsChange();
			this.angledoom = angledoom;
			this.anglerad = Angle2D.DegToRad(angledoom);
		}

		/// <summary>
		/// Sets the angle in radians.
		/// </summary>
		public void SetAngleRadians(double anglerad)
		{
			BeforeFieldsChange();
			this.anglerad = anglerad;
			this.angledoom = (int)Angle2D.RadToDeg(anglerad);
		}

		/// <summary>
		/// Sets the pitch angle.
		/// </summary>
		public void SetPitch(int pitch)
		{
			BeforeFieldsChange();
			this.pitch = pitch;
			this.pitchrad = Angle2D.DegToRad(pitch);
		}

		/// <summary>
		/// Sets the roll angle.
		/// </summary>
		public void SetRoll(int roll)
		{
			BeforeFieldsChange();
			this.roll = roll;
			this.rollrad = Angle2D.DegToRad(roll);
		}

		/// <summary>
		/// Sets a flag value.
		/// </summary>
		public void SetFlag(string flagname, bool value)
		{
			BeforeFieldsChange();
			if(value)
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
		/// Returns a string representation of this thing.
		/// </summary>
		public override string ToString()
		{
			return $"Thing type {type} at ({pos.x}, {pos.y}, {pos.z})";
		}

		#endregion
	}
}
