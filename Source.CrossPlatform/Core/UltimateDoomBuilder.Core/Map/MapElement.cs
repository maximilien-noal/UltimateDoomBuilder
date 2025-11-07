
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

#endregion

namespace UltimateDoomBuilder.Core.Map
{
	public enum MapElementType
	{
		UNKNOWN,
		VERTEX,
		SIDEDEF,
		LINEDEF,
		SECTOR,
		THING
	}
	
	public abstract class MapElement : IDisposable
	{
		#region ================== Variables
		
		// List index
		protected int listindex;
		
		// Universal fields
		private UniFields fields;
		
		// Marking
		protected bool marked;
		
		// Disposing
		protected bool isdisposed;
		
		// Error Ignoring
		private List<Type> ignorederrorchecks;

		// Hashing
		private static int hashcounter;
		private readonly int hashcode;

		// Element type
		protected MapElementType elementtype;
		
		#endregion
		
		#region ================== Properties

		public int Index { get { return listindex; } internal set { listindex = value; } }
		public UniFields Fields { get { return fields; } }
		public bool Marked { get { return marked; } set { marked = value; } }
		public bool IsDisposed { get { return isdisposed; } }
		public List<Type> IgnoredErrorChecks { get { return ignorederrorchecks; } }
		public MapElementType ElementType { get { return elementtype; } }

		#endregion

		#region ================== Constructor / Disposer

		// Constructor
		protected MapElement()
		{
			// Initialize
			fields = new UniFields(this);
			ignorederrorchecks = new List<Type>();
			hashcode = hashcounter++;
		}

		// Disposer
		public virtual void Dispose()
		{
			if(!isdisposed)
			{
				// Clean up
				fields.Owner = null;

				// Done
				isdisposed = true;
			}
		}

		#endregion

		#region ================== Methods

		/// <summary>
		/// This is called before a change is made to fields.
		/// </summary>
		public virtual void BeforeFieldsChange()
		{
			// Override in derived classes to implement undo/redo support
		}

		/// <summary>
		/// This returns the unique hash code for this element.
		/// </summary>
		public override int GetHashCode()
		{
			return hashcode;
		}

		#endregion
	}
}
