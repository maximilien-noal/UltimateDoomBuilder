using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia;
using System;
using UltimateDoomBuilder.Avalonia.Controls;

namespace UltimateDoomBuilder.Avalonia;

public class MainWindow : Window
{
    private DoomRendererControl? _rendererControl;
    private TextBlock? _statusText;
    
    public MainWindow()
    {
        InitializeWindow();
    }

    private void InitializeWindow()
    {
        // Configure window properties without XAML
        Title = "Ultimate Doom Builder - Cross Platform";
        Width = 1280;
        Height = 720;
        
        // Create main layout with DockPanel
        var mainPanel = new DockPanel
        {
            Background = Brushes.DarkGray
        };
        
        // Create menu bar
        var menuBar = CreateMenuBar();
        DockPanel.SetDock(menuBar, Dock.Top);
        mainPanel.Children.Add(menuBar);
        
        // Create toolbar
        var toolbar = CreateToolbar();
        DockPanel.SetDock(toolbar, Dock.Top);
        mainPanel.Children.Add(toolbar);
        
        // Create status bar (bottom)
        var statusBar = CreateStatusBar();
        DockPanel.SetDock(statusBar, Dock.Bottom);
        mainPanel.Children.Add(statusBar);
        
        // Create renderer control for level preview (fills remaining space)
        _rendererControl = new DoomRendererControl
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch
        };
        
        mainPanel.Children.Add(_rendererControl);
        Content = mainPanel;
    }

    private Menu CreateMenuBar()
    {
        var menu = new Menu
        {
            Background = Brushes.White
        };

        // File Menu
        var fileMenu = new MenuItem { Header = "File" };
        fileMenu.Items.Add(new MenuItem { Header = "New Map", InputGesture = new KeyGesture(Key.N, KeyModifiers.Control) });
        fileMenu.Items.Add(new MenuItem { Header = "Open Map...", InputGesture = new KeyGesture(Key.O, KeyModifiers.Control) });
        fileMenu.Items.Add(new MenuItem { Header = "Save Map", InputGesture = new KeyGesture(Key.S, KeyModifiers.Control) });
        fileMenu.Items.Add(new MenuItem { Header = "Save Map As..." });
        fileMenu.Items.Add(new Separator());
        fileMenu.Items.Add(new MenuItem { Header = "Exit" });
        menu.Items.Add(fileMenu);

        // Edit Menu
        var editMenu = new MenuItem { Header = "Edit" };
        editMenu.Items.Add(new MenuItem { Header = "Undo", InputGesture = new KeyGesture(Key.Z, KeyModifiers.Control) });
        editMenu.Items.Add(new MenuItem { Header = "Redo", InputGesture = new KeyGesture(Key.Y, KeyModifiers.Control) });
        editMenu.Items.Add(new Separator());
        editMenu.Items.Add(new MenuItem { Header = "Cut", InputGesture = new KeyGesture(Key.X, KeyModifiers.Control) });
        editMenu.Items.Add(new MenuItem { Header = "Copy", InputGesture = new KeyGesture(Key.C, KeyModifiers.Control) });
        editMenu.Items.Add(new MenuItem { Header = "Paste", InputGesture = new KeyGesture(Key.V, KeyModifiers.Control) });
        editMenu.Items.Add(new Separator());
        editMenu.Items.Add(new MenuItem { Header = "Select All", InputGesture = new KeyGesture(Key.A, KeyModifiers.Control) });
        editMenu.Items.Add(new MenuItem { Header = "Clear Selection" });
        menu.Items.Add(editMenu);

        // View Menu
        var viewMenu = new MenuItem { Header = "View" };
        viewMenu.Items.Add(new MenuItem { Header = "Zoom In", InputGesture = new KeyGesture(Key.Add, KeyModifiers.Control) });
        viewMenu.Items.Add(new MenuItem { Header = "Zoom Out", InputGesture = new KeyGesture(Key.Subtract, KeyModifiers.Control) });
        viewMenu.Items.Add(new MenuItem { Header = "Zoom to Fit" });
        viewMenu.Items.Add(new Separator());
        viewMenu.Items.Add(new MenuItem { Header = "Grid Settings..." });
        viewMenu.Items.Add(new MenuItem { Header = "Visual Mode", InputGesture = new KeyGesture(Key.Q) });
        menu.Items.Add(viewMenu);

        // Mode Menu
        var modeMenu = new MenuItem { Header = "Mode" };
        modeMenu.Items.Add(new MenuItem { Header = "Vertices Mode", InputGesture = new KeyGesture(Key.V) });
        modeMenu.Items.Add(new MenuItem { Header = "Linedefs Mode", InputGesture = new KeyGesture(Key.L) });
        modeMenu.Items.Add(new MenuItem { Header = "Sectors Mode", InputGesture = new KeyGesture(Key.S) });
        modeMenu.Items.Add(new MenuItem { Header = "Things Mode", InputGesture = new KeyGesture(Key.T) });
        menu.Items.Add(modeMenu);

        // Tools Menu
        var toolsMenu = new MenuItem { Header = "Tools" };
        toolsMenu.Items.Add(new MenuItem { Header = "Draw Lines" });
        toolsMenu.Items.Add(new MenuItem { Header = "Draw Rectangle" });
        toolsMenu.Items.Add(new MenuItem { Header = "Draw Circle" });
        toolsMenu.Items.Add(new Separator());
        toolsMenu.Items.Add(new MenuItem { Header = "Preferences..." });
        menu.Items.Add(toolsMenu);

        // Help Menu
        var helpMenu = new MenuItem { Header = "Help" };
        helpMenu.Items.Add(new MenuItem { Header = "Documentation" });
        helpMenu.Items.Add(new MenuItem { Header = "About..." });
        menu.Items.Add(helpMenu);

        return menu;
    }

    private StackPanel CreateToolbar()
    {
        var toolbar = new StackPanel
        {
            Background = Brushes.LightGray,
            Height = 36,
            Orientation = Orientation.Horizontal,
            Spacing = 4
        };

        // Add toolbar buttons (placeholders for now)
        toolbar.Children.Add(CreateToolbarButton("New"));
        toolbar.Children.Add(CreateToolbarButton("Open"));
        toolbar.Children.Add(CreateToolbarButton("Save"));
        toolbar.Children.Add(CreateToolbarSeparator());
        toolbar.Children.Add(CreateToolbarButton("Undo"));
        toolbar.Children.Add(CreateToolbarButton("Redo"));
        toolbar.Children.Add(CreateToolbarSeparator());
        toolbar.Children.Add(CreateToolbarButton("Vertices"));
        toolbar.Children.Add(CreateToolbarButton("Linedefs"));
        toolbar.Children.Add(CreateToolbarButton("Sectors"));
        toolbar.Children.Add(CreateToolbarButton("Things"));

        return toolbar;
    }

    private Button CreateToolbarButton(string text)
    {
        return new Button
        {
            Content = text,
            Width = 60,
            Height = 28,
            Margin = new Thickness(2),
            Background = Brushes.White,
            BorderBrush = Brushes.Gray,
            BorderThickness = new Thickness(1)
        };
    }

    private Border CreateToolbarSeparator()
    {
        return new Border
        {
            Width = 1,
            Height = 28,
            Background = Brushes.Gray,
            Margin = new Thickness(4, 0)
        };
    }

    private Border CreateStatusBar()
    {
        var statusBar = new Border
        {
            Background = Brushes.White,
            BorderBrush = Brushes.Gray,
            BorderThickness = new Thickness(0, 1, 0, 0),
            Height = 24
        };

        var statusPanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 10,
            Margin = new Thickness(5, 0)
        };

        _statusText = new TextBlock
        {
            Text = "Ready",
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 12
        };

        statusPanel.Children.Add(_statusText);
        statusBar.Child = statusPanel;

        return statusBar;
    }

    public void SetStatusText(string text)
    {
        if (_statusText != null)
        {
            _statusText.Text = text;
        }
    }
}