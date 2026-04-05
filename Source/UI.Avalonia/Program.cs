using System;
using Avalonia;

namespace CodeImp.DoomBuilder.Avalonia
{
	/// <summary>
	/// Cross-platform entry point for UltimateDoomBuilder using AvaloniaUI.
	/// This replaces the WinForms-based entry point for Linux and macOS.
	/// </summary>
	public class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
		}

		public static AppBuilder BuildAvaloniaApp()
			=> AppBuilder.Configure<App>()
				.UsePlatformDetect()
				.LogToTrace();
	}
}
