using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace CodeImp.DoomBuilder.Avalonia.Windows
{
	/// <summary>
	/// Main application window - Avalonia equivalent of WinForms MainForm.
	/// Built entirely in code-behind (no AXAML).
	/// 
	/// This is the initial skeleton that will be progressively filled with
	/// ported functionality from Source/Core/Windows/MainForm.cs.
	/// </summary>
	public class MainWindow : Window
	{
		// Main layout panels
		private readonly DockPanel _rootPanel;
		private readonly Menu _mainMenu;
		private readonly StackPanel _toolBar;
		private readonly Panel _renderPanel;
		private readonly StackPanel _statusBar;

		public MainWindow()
		{
			Title = "Ultimate Doom Builder";
			Width = 1280;
			Height = 900;
			WindowStartupLocation = WindowStartupLocation.CenterScreen;

			// Build the UI tree in code
			_rootPanel = new DockPanel();

			// Menu bar
			_mainMenu = CreateMainMenu();
			DockPanel.SetDock(_mainMenu, Dock.Top);
			_rootPanel.Children.Add(_mainMenu);

			// Toolbar
			_toolBar = CreateToolBar();
			DockPanel.SetDock(_toolBar, Dock.Top);
			_rootPanel.Children.Add(_toolBar);

			// Status bar
			_statusBar = CreateStatusBar();
			DockPanel.SetDock(_statusBar, Dock.Bottom);
			_rootPanel.Children.Add(_statusBar);

			// Main rendering area (will host NativeControlHost for OpenGL)
			_renderPanel = new Panel
			{
				Background = Brushes.Black,
			};
			_rootPanel.Children.Add(_renderPanel);

			Content = _rootPanel;
		}

		private Menu CreateMainMenu()
		{
			return new Menu
			{
				Items =
				{
					new MenuItem
					{
						Header = "_File",
						Items =
						{
							new MenuItem { Header = "_New Map..." },
							new MenuItem { Header = "_Open Map..." },
							new Separator(),
							new MenuItem { Header = "_Save Map" },
							new MenuItem { Header = "Save Map _As..." },
							new Separator(),
							new MenuItem { Header = "E_xit" },
						}
					},
					new MenuItem
					{
						Header = "_Edit",
						Items =
						{
							new MenuItem { Header = "_Undo" },
							new MenuItem { Header = "_Redo" },
							new Separator(),
							new MenuItem { Header = "Cu_t" },
							new MenuItem { Header = "_Copy" },
							new MenuItem { Header = "_Paste" },
						}
					},
					new MenuItem
					{
						Header = "_View",
						Items =
						{
							new MenuItem { Header = "_Zoom In" },
							new MenuItem { Header = "Zoom _Out" },
							new MenuItem { Header = "Zoom to _Fit" },
							new Separator(),
							new MenuItem { Header = "_Grid Setup..." },
						}
					},
					new MenuItem
					{
						Header = "_Mode",
						Items =
						{
							new MenuItem { Header = "_Vertices Mode" },
							new MenuItem { Header = "_Linedefs Mode" },
							new MenuItem { Header = "_Sectors Mode" },
							new MenuItem { Header = "_Things Mode" },
							new Separator(),
							new MenuItem { Header = "_Visual Mode" },
						}
					},
					new MenuItem
					{
						Header = "_Tools",
						Items =
						{
							new MenuItem { Header = "_Preferences..." },
							new MenuItem { Header = "_Game Configuration..." },
						}
					},
					new MenuItem
					{
						Header = "_Help",
						Items =
						{
							new MenuItem { Header = "_About Ultimate Doom Builder" },
						}
					},
				}
			};
		}

		private StackPanel CreateToolBar()
		{
			return new StackPanel
			{
				Orientation = Orientation.Horizontal,
				Background = new SolidColorBrush(Color.FromRgb(240, 240, 240)),
				Height = 28,
				Children =
				{
					new Button { Content = "New", Margin = new Thickness(2) },
					new Button { Content = "Open", Margin = new Thickness(2) },
					new Button { Content = "Save", Margin = new Thickness(2) },
					new Separator(),
					new Button { Content = "Undo", Margin = new Thickness(2) },
					new Button { Content = "Redo", Margin = new Thickness(2) },
				}
			};
		}

		private StackPanel CreateStatusBar()
		{
			return new StackPanel
			{
				Orientation = Orientation.Horizontal,
				Background = new SolidColorBrush(Color.FromRgb(0, 122, 204)),
				Height = 24,
				Children =
				{
					new TextBlock
					{
						Text = "Ready",
						Foreground = Brushes.White,
						VerticalAlignment = VerticalAlignment.Center,
						Margin = new Thickness(8, 0),
					},
				}
			};
		}
	}
}
