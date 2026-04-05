using System;
using System.Runtime.InteropServices;

namespace CodeImp.DoomBuilder.Platform
{
	/// <summary>
	/// Provides platform-specific mouse input handling (replaces Windows Raw Input).
	/// </summary>
	public interface IPlatformMouseInput : IDisposable
	{
		/// <summary>
		/// Gets the accumulated X delta since last read.
		/// </summary>
		float GetDeltaX();

		/// <summary>
		/// Gets the accumulated Y delta since last read.
		/// </summary>
		float GetDeltaY();

		/// <summary>
		/// Shows or hides the system cursor.
		/// </summary>
		void SetCursorVisible(bool visible);
	}
}
