using System;

namespace CodeImp.DoomBuilder.Platform
{
	/// <summary>
	/// Provides platform-specific file lock detection (replaces Windows Restart Manager).
	/// </summary>
	public interface IPlatformFileLock
	{
		/// <summary>
		/// Checks if a file is locked by another process.
		/// </summary>
		bool IsFileLocked(string filePath);
	}
}
