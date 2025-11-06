
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
using UltimateDoomBuilder.Core.Types;

#endregion

namespace UltimateDoomBuilder.Core.Map
{
	public class UniValue
	{
		#region ================== Variables
		
		private object value;
		private int type;

		#endregion

		#region ================== Properties

		public object Value
		{
			get
			{
				return this.value;
			}
			
			set
			{
				// Value may only be a primitive type
				if((!(value is int) && !(value is double) && !(value is string) && !(value is bool)) || (value == null))
					throw new ArgumentException("Universal field values can only be of type int, double, string or bool.");
				
				this.value = value;
			}
		}
		
		public int Type { get { return this.type; } set { this.type = value; } }

		#endregion

		#region ================== Constructor

		// Constructor
		public UniValue(int type, object value)
		{
			// Value may only be a primitive type
			if ((!(value is int) && !(value is double) && !(value is string) && !(value is bool)) || (value == null))
				throw new ArgumentException("Universal field values can only be of type int, double, string or bool.");

			this.type = type;
			this.value = value;
			
			// We have no destructor
			GC.SuppressFinalize(this);
		}

		// Constructor
		public UniValue(UniversalType type, object value)
		{
			// Value may only be a primitive type
			if ((!(value is int) && !(value is double) && !(value is string) && !(value is bool)) || (value == null))
				throw new ArgumentException("Universal field values can only be of type int, double, string or bool.");

			this.type = (int)type;
			this.value = value;

			// We have no destructor
			GC.SuppressFinalize(this);
		}

		// Copy constructor
		public UniValue(UniValue v)
		{
			this.type = v.type;
			this.value = v.value;

			// We have no destructor
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}
