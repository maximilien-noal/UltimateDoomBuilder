using System;
using System.Runtime.Versioning;

namespace CodeImp.DoomBuilder.Platform
{
	/// <summary>
	/// Resolves platform-specific service implementations at runtime.
	/// </summary>
	public static class PlatformFactory
	{
		private static IPlatformSettings _settings;
		private static IPlatformFileLock _fileLock;
		private static IPlatformNativeWindow _nativeWindow;

		/// <summary>
		/// Gets the platform settings provider.
		/// </summary>
		public static IPlatformSettings Settings
		{
			get
			{
				if (_settings == null)
					_settings = CreateSettings();
				return _settings;
			}
		}

		/// <summary>
		/// Gets the platform file lock provider.
		/// </summary>
		public static IPlatformFileLock FileLock
		{
			get
			{
				if (_fileLock == null)
					_fileLock = CreateFileLock();
				return _fileLock;
			}
		}

		/// <summary>
		/// Gets the platform native window provider.
		/// </summary>
		public static IPlatformNativeWindow NativeWindow
		{
			get
			{
				if (_nativeWindow == null)
					_nativeWindow = CreateNativeWindow();
				return _nativeWindow;
			}
		}

		/// <summary>
		/// Allows overriding the default platform implementations (for testing or custom platforms).
		/// </summary>
		public static void Override(IPlatformSettings settings = null, IPlatformFileLock fileLock = null, IPlatformNativeWindow nativeWindow = null)
		{
			if (settings != null) _settings = settings;
			if (fileLock != null) _fileLock = fileLock;
			if (nativeWindow != null) _nativeWindow = nativeWindow;
		}

		private static IPlatformSettings CreateSettings()
		{
			if (OperatingSystem.IsWindows())
				return new WindowsPlatformSettings();
			else
				return new CrossPlatformSettings();
		}

		private static IPlatformFileLock CreateFileLock()
		{
			if (OperatingSystem.IsWindows())
				return new WindowsFileLock();
			else
				return new UnixFileLock();
		}

		private static IPlatformNativeWindow CreateNativeWindow()
		{
			if (OperatingSystem.IsWindows())
				return new WindowsNativeWindow();
			else
				return new NoOpNativeWindow();
		}
	}

	#region Windows Implementations

	[SupportedOSPlatform("windows")]
	internal class WindowsPlatformSettings : IPlatformSettings
	{
		public string GetValue(string key, string defaultValue)
		{
			var value = Microsoft.Win32.Registry.GetValue(
				@"HKEY_CURRENT_USER\SOFTWARE\Ultimate Doom Builder", key, defaultValue);
			return value?.ToString() ?? defaultValue;
		}

		public void SetValue(string key, string value)
		{
			Microsoft.Win32.Registry.SetValue(
				@"HKEY_CURRENT_USER\SOFTWARE\Ultimate Doom Builder", key, value);
		}

		public bool HasKey(string key)
		{
			return Microsoft.Win32.Registry.GetValue(
				@"HKEY_CURRENT_USER\SOFTWARE\Ultimate Doom Builder", key, null) != null;
		}

		public void DeleteKey(string key)
		{
			using (var regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Ultimate Doom Builder", true))
			{
				regKey?.DeleteValue(key, false);
			}
		}
	}

	[SupportedOSPlatform("windows")]
	internal class WindowsFileLock : IPlatformFileLock
	{
		public bool IsFileLocked(string filePath)
		{
			try
			{
				using (System.IO.File.Open(filePath, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.None))
				{
					return false;
				}
			}
			catch (System.IO.IOException)
			{
				return true;
			}
		}
	}

	[SupportedOSPlatform("windows")]
	internal class WindowsNativeWindow : IPlatformNativeWindow
	{
		[System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "LockWindowUpdate")]
		private static extern bool NativeLockWindowUpdate(IntPtr hWndLock);

		public void LockWindowUpdate(IntPtr hWnd)
		{
			NativeLockWindowUpdate(hWnd);
		}

		public void UnlockWindowUpdate()
		{
			NativeLockWindowUpdate(IntPtr.Zero);
		}

		public void SystemBeep()
		{
			System.Media.SystemSounds.Beep.Play();
		}
	}

	#endregion

	#region Cross-Platform Implementations

	internal class CrossPlatformSettings : IPlatformSettings
	{
		private readonly string _settingsPath;
		private readonly System.Collections.Generic.Dictionary<string, string> _cache;

		public CrossPlatformSettings()
		{
			string configDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			if (string.IsNullOrEmpty(configDir))
				configDir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config");

			string appDir = System.IO.Path.Combine(configDir, "UltimateDoomBuilder");
			System.IO.Directory.CreateDirectory(appDir);
			_settingsPath = System.IO.Path.Combine(appDir, "settings.json");

			_cache = new System.Collections.Generic.Dictionary<string, string>();
			LoadSettings();
		}

		private void LoadSettings()
		{
			if (System.IO.File.Exists(_settingsPath))
			{
				try
				{
					string json = System.IO.File.ReadAllText(_settingsPath);
					// Simple JSON parsing for key-value pairs
					var dict = System.Text.Json.JsonSerializer.Deserialize<System.Collections.Generic.Dictionary<string, string>>(json);
					if (dict != null)
					{
						foreach (var kvp in dict)
							_cache[kvp.Key] = kvp.Value;
					}
				}
				catch { }
			}
		}

		private void SaveSettings()
		{
			try
			{
				string json = System.Text.Json.JsonSerializer.Serialize(_cache,
					new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
				System.IO.File.WriteAllText(_settingsPath, json);
			}
			catch { }
		}

		public string GetValue(string key, string defaultValue)
		{
			return _cache.TryGetValue(key, out string value) ? value : defaultValue;
		}

		public void SetValue(string key, string value)
		{
			_cache[key] = value;
			SaveSettings();
		}

		public bool HasKey(string key) => _cache.ContainsKey(key);

		public void DeleteKey(string key)
		{
			_cache.Remove(key);
			SaveSettings();
		}
	}

	internal class UnixFileLock : IPlatformFileLock
	{
		public bool IsFileLocked(string filePath)
		{
			try
			{
				using (System.IO.File.Open(filePath, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.None))
				{
					return false;
				}
			}
			catch (System.IO.IOException)
			{
				return true;
			}
		}
	}

	internal class NoOpNativeWindow : IPlatformNativeWindow
	{
		public void LockWindowUpdate(IntPtr hWnd) { }
		public void UnlockWindowUpdate() { }
		public void SystemBeep() { Console.Beep(); }
	}

	#endregion
}
