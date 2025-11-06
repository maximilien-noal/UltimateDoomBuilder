using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using UltimateDoomBuilder.Avalonia.Controls;

namespace UltimateDoomBuilder.Avalonia;

public class MainWindow : Window
{
    private DoomRendererControl? _rendererControl;
    
    public MainWindow()
    {
        InitializeWindow();
    }

    private void InitializeWindow()
    {
        // Configure window properties without XAML
        Title = "Ultimate Doom Builder - Cross Platform";
        Width = 1200;
        Height = 800;
        
        // Create main layout with DockPanel
        var mainPanel = new DockPanel
        {
            Background = Brushes.DarkGray
        };
        
        // Create top toolbar placeholder
        var toolbar = new StackPanel
        {
            Background = Brushes.Gray,
            Height = 40,
            Orientation = Orientation.Horizontal
        };
        
        var titleText = new TextBlock
        {
            Text = "Ultimate Doom Builder - Avalonia Edition",
            FontSize = 14,
            Foreground = Brushes.White,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new global::Avalonia.Thickness(10, 0)
        };
        
        toolbar.Children.Add(titleText);
        DockPanel.SetDock(toolbar, Dock.Top);
        mainPanel.Children.Add(toolbar);
        
        // Create renderer control for level preview
        _rendererControl = new DoomRendererControl
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch
        };
        
        mainPanel.Children.Add(_rendererControl);
        Content = mainPanel;
    }
}