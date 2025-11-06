#region ================== Namespaces

using System;
using System.Collections.Generic;
using UltimateDoomBuilder.Core.Types;

#endregion

namespace UltimateDoomBuilder.Core.Map
{
	/// <summary>
	/// List of universal fields and their values.
	/// </summary>
	[Serializable]
	public class UniFields : Dictionary<string, UniValue>
	{
		#region ================== Variables

		// Owner of this list
		private MapElement? owner;

		#endregion

		#region ================== Properties
		
		public MapElement? Owner { get { return owner; } internal set { owner = value; } }

		#endregion

		#region ================== Constructors

		// New constructor
		public UniFields() : base(2) { }

		// New constructor
		public UniFields(int capacity) : base(capacity) { }

		// Copy constructor (makes a deep copy)
		public UniFields(UniFields copyfrom) : base(copyfrom.Count)
		{
			foreach(KeyValuePair<string, UniValue> v in copyfrom)
				this.Add(v.Key, new UniValue(v.Value));
		}
		
		// New constructor
		public UniFields(MapElement owner) : base(2)
		{
			this.owner = owner;
		}

		// New constructor
		public UniFields(MapElement owner, int capacity) : base(capacity)
		{
			this.owner = owner;
		}

		// Copy constructor
		public UniFields(MapElement owner, UniFields copyfrom) : base(copyfrom.Count)
		{
			this.owner = owner;

			foreach(KeyValuePair<string, UniValue> v in copyfrom)
				this.Add(v.Key, new UniValue(v.Value));
		}

		#endregion

		#region ================== Methods

		/// <summary>Call this before making changes to the fields, or they may not be updated correctly with undo/redo!</summary>
		public void BeforeFieldsChange()
		{
			if(owner != null) owner.BeforeFieldsChange();
		}
		
		/// <summary>This returns the value of a field by name, or returns the specified value when no such field exists or the field value fails to convert to the same datatype.</summary>
		public T GetValue<T>(string fieldname, T defaultvalue)
		{
			if(!this.ContainsKey(fieldname)) return defaultvalue;

			try
			{
				T? val = (T?)this[fieldname].Value;
				return val ?? defaultvalue;
			}
			catch(InvalidCastException)
			{
				return defaultvalue;
			}
		}

		#endregion

		#region ================== Static methods

		// float
		public static void SetFloat(UniFields? fields, string key, double value) { SetFloat(fields, key, value, 0.0); }
		public static void SetFloat(UniFields? fields, string key, double value, double defaultvalue)
		{
			if(fields == null) return;
			if(value != defaultvalue)
			{
				if(!fields.ContainsKey(key)) fields.Add(key, new UniValue(UniversalType.Float, value));
				else fields[key].Value = value;
			}
			// Don't save default value
			else if(fields.ContainsKey(key)) 
			{
				fields.Remove(key);
			}
		}

		public static double GetFloat(UniFields? fields, string key) { return GetFloat(fields, key, 0.0); }
		public static double GetFloat(UniFields? fields, string key, double defaultvalue)
		{
			if(fields == null) return defaultvalue;
			return fields.GetValue(key, defaultvalue);
		}

		// int
		public static void SetInteger(UniFields? fields, string key, int value) { SetInteger(fields, key, value, 0); }
		public static void SetInteger(UniFields? fields, string key, int value, int defaultvalue)
		{
			if(fields == null) return;
			if(value != defaultvalue)
			{
				if(!fields.ContainsKey(key)) fields.Add(key, new UniValue(UniversalType.Integer, value));
				else fields[key].Value = value;
			}
			// Don't save default value
			else if(fields.ContainsKey(key)) 
			{
				fields.Remove(key);
			}
		}

		public static int GetInteger(UniFields? fields, string key) { return GetInteger(fields, key, 0); }
		public static int GetInteger(UniFields? fields, string key, int defaultvalue)
		{
			if(fields == null) return defaultvalue;
			return fields.GetValue(key, defaultvalue);
		}

		// String
		public static void SetString(UniFields? fields, string key, string value) { SetString(fields, key, value, ""); }
		public static void SetString(UniFields? fields, string key, string value, string defaultvalue)
		{
			if(fields == null) return;
			if(value != defaultvalue)
			{
				if(!fields.ContainsKey(key)) fields.Add(key, new UniValue(UniversalType.String, value));
				else fields[key].Value = value;
			}
			// Don't save default value
			else if(fields.ContainsKey(key)) 
			{
				fields.Remove(key);
			}
		}

		public static string GetString(UniFields? fields, string key) { return GetString(fields, key, ""); }
		public static string GetString(UniFields? fields, string key, string defaultvalue)
		{
			if(fields == null) return defaultvalue;
			return fields.GetValue(key, defaultvalue);
		}

		// Boolean
		public static void SetBoolean(UniFields? fields, string key, bool value) { SetBoolean(fields, key, value, false); }
		public static void SetBoolean(UniFields? fields, string key, bool value, bool defaultvalue)
		{
			if(fields == null) return;
			if(value != defaultvalue)
			{
				if(!fields.ContainsKey(key)) fields.Add(key, new UniValue(UniversalType.Boolean, value));
				else fields[key].Value = value;
			}
			// Don't save default value
			else if(fields.ContainsKey(key)) 
			{
				fields.Remove(key);
			}
		}

		public static bool GetBoolean(UniFields? fields, string key) { return GetBoolean(fields, key, false); }
		public static bool GetBoolean(UniFields? fields, string key, bool defaultvalue)
		{
			if(fields == null) return defaultvalue;
			return fields.GetValue(key, defaultvalue);
		}

		#endregion
	}
}
