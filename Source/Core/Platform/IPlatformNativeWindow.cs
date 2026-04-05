using System;

namespace CodeImp.DoomBuilder.Platform
{
	/// <summary>
	/// Provides platform-specific native window operations (replaces SendMessage/PostMessage).
	/// </summary>
	public interface IPlatformNativeWindow
	{
		/// <summary>
		/// Locks window updates (prevents repainting) for the given window handle.
		/// </summary>
		void LockWindowUpdate(IntPtr hWnd);

		/// <summary>
		/// Unlocks window updates.
		/// </summary>
		void UnlockWindowUpdate();

		/// <summary>
		/// Plays a system beep.
		/// </summary>
		void SystemBeep();
	}
}
