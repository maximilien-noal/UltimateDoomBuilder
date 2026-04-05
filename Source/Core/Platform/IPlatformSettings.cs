using System;

namespace CodeImp.DoomBuilder.Platform
{
	/// <summary>
	/// Provides platform-specific settings storage (replaces Windows Registry usage).
	/// </summary>
	public interface IPlatformSettings
	{
		string GetValue(string key, string defaultValue);
		void SetValue(string key, string value);
		bool HasKey(string key);
		void DeleteKey(string key);
	}
}
