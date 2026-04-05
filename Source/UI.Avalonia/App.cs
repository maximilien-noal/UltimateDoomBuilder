using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Themes.Fluent;

namespace CodeImp.DoomBuilder.Avalonia
{
	/// <summary>
	/// Avalonia Application class. All UI is built in code-behind (no AXAML).
	/// </summary>
	public class App : Application
	{
		public override void Initialize()
		{
			Styles.Add(new FluentTheme());
		}

		public override void OnFrameworkInitializationCompleted()
		{
			if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
			{
				desktop.MainWindow = new Windows.MainWindow();
			}

			base.OnFrameworkInitializationCompleted();
		}
	}
}
