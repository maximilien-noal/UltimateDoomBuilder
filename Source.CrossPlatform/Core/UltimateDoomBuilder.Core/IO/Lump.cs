
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
using System.IO;
using System.Text;

#endregion

namespace UltimateDoomBuilder.Core.IO
{
	/// <summary>
	/// Represents a lump (data entry) in a WAD file.
	/// Lumps are named blocks of data that contain maps, textures, sounds, etc.
	/// </summary>
	public class Lump : IDisposable
	{
		#region ================== Constants

		// Allowed characters in a map lump name
		public const string MAP_LUMP_NAME_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";
		public const int MAX_LUMP_NAME_LENGTH = 8;

		#endregion

		#region ================== Variables

		// Data info
		private string name;
		private readonly byte[] fixedname;
		private readonly int offset;
		private readonly int length;
		private readonly Stream? dataStream;

		// Disposing
		private bool isdisposed;

		#endregion

		#region ================== Properties

		public string Name { get { return name; } }
		public byte[] FixedName { get { return fixedname; } }
		public int Offset { get { return offset; } }
		public int Length { get { return length; } }
		public bool IsDisposed { get { return isdisposed; } }

		#endregion

		#region ================== Constructor / Disposer

		/// <summary>
		/// Creates a new lump.
		/// </summary>
		public Lump(string name, int offset, int length, Stream? dataStream = null)
		{
			this.name = name.ToUpperInvariant();
			this.fixedname = MakeFixedName(this.name);
			this.offset = offset;
			this.length = length;
			this.dataStream = dataStream;

			// We have no destructor
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Creates a lump from fixed name bytes.
		/// </summary>
		public Lump(byte[] fixedname, int offset, int length, Stream? dataStream = null)
		{
			this.fixedname = fixedname;
			this.name = MakeNormalName(fixedname).ToUpperInvariant();
			this.offset = offset;
			this.length = length;
			this.dataStream = dataStream;

			// We have no destructor
			GC.SuppressFinalize(this);
		}

		// Disposer
		public void Dispose()
		{
			if (!isdisposed)
			{
				isdisposed = true;
			}
		}

		#endregion

		#region ================== Static Methods

		/// <summary>
		/// Converts a fixed-length byte array to a lump name string.
		/// </summary>
		public static string MakeNormalName(byte[] fixedname)
		{
			if (fixedname == null || fixedname.Length == 0)
				return string.Empty;

			// Find the end of the string (first null byte)
			int length = 0;
			while (length < fixedname.Length && fixedname[length] != 0)
				length++;

			// Convert to string
			if (length == 0)
				return string.Empty;

			return Encoding.ASCII.GetString(fixedname, 0, length);
		}

		/// <summary>
		/// Converts a lump name string to a fixed-length byte array.
		/// </summary>
		public static byte[] MakeFixedName(string name)
		{
			byte[] fixedname = new byte[MAX_LUMP_NAME_LENGTH];

			if (string.IsNullOrEmpty(name))
				return fixedname;

			// Limit to 8 characters and convert to uppercase
			string upperName = name.ToUpperInvariant();
			if (upperName.Length > MAX_LUMP_NAME_LENGTH)
				upperName = upperName.Substring(0, MAX_LUMP_NAME_LENGTH);

			// Convert to bytes
			byte[] nameBytes = Encoding.ASCII.GetBytes(upperName);
			Array.Copy(nameBytes, fixedname, Math.Min(nameBytes.Length, MAX_LUMP_NAME_LENGTH));

			return fixedname;
		}

		/// <summary>
		/// Checks if a lump name is valid for map lumps.
		/// </summary>
		public static bool IsValidMapLumpName(string name)
		{
			if (string.IsNullOrEmpty(name))
				return false;

			if (name.Length > MAX_LUMP_NAME_LENGTH)
				return false;

			foreach (char c in name)
			{
				if (!MAP_LUMP_NAME_CHARS.Contains(c))
					return false;
			}

			return true;
		}

		#endregion

		#region ================== Methods

		/// <summary>
		/// Reads all data from this lump.
		/// </summary>
		public byte[]? ReadAllBytes()
		{
			if (dataStream == null || length == 0)
				return null;

			try
			{
				byte[] data = new byte[length];
				dataStream.Position = offset;
				int bytesRead = dataStream.Read(data, 0, length);

				if (bytesRead != length)
					return null;

				return data;
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Returns a string representation of this lump.
		/// </summary>
		public override string ToString()
		{
			return $"Lump '{name}' ({length} bytes at offset {offset})";
		}

		#endregion
	}
}
