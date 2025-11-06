# Ultimate Doom Builder - Cross-Platform Migration

This directory contains the cross-platform migration of Ultimate Doom Builder from Windows Forms (.NET Framework 4.7.2) to Avalonia UI (.NET 8).

## Architecture

### Key Design Decisions

1. **No XAML**: Following the requirement, the UI is built entirely in C# code-behind, maintaining a similar approach to the original WinForms implementation.

2. **Non-MVVM**: The architecture preserves the original non-MVVM pattern to stay close to the existing codebase structure.

3. **.NET 8 Runtime**: All projects target .NET 8.0 for modern, cross-platform compatibility.

4. **OpenGL Rendering**: Custom `OpenGLControlBase` class provides the foundation for cross-platform OpenGL rendering, similar to the original `RenderTargetControl`.

## Project Structure

```
Source.CrossPlatform/
├── Core/
│   └── UltimateDoomBuilder.Avalonia/     # Main Avalonia application
│       ├── Controls/
│       │   ├── OpenGLControlBase.cs      # Base class for OpenGL controls
│       │   └── DoomRendererControl.cs    # Doom level renderer
│       ├── App.axaml.cs                  # Application entry (no XAML)
│       ├── MainWindow.axaml.cs           # Main window (no XAML)
│       └── Program.cs                    # Entry point

Tests/
├── UltimateDoomBuilder.Core.Tests/       # xUnit tests for core functionality
└── UltimateDoomBuilder.UI.Tests/         # UI automation tests (future)
```

## Building

### Prerequisites

- .NET 8 SDK or later
- Linux: Standard build tools (make, g++, etc.)
- Windows: Visual Studio 2022 or later
- macOS: Xcode command line tools

### Build Commands

```bash
# Build the Avalonia application
dotnet build Source.CrossPlatform/Core/UltimateDoomBuilder.Avalonia/UltimateDoomBuilder.Avalonia.csproj

# Run tests
dotnet test Tests/UltimateDoomBuilder.Core.Tests/UltimateDoomBuilder.Core.Tests.csproj

# Build entire cross-platform solution
dotnet build UltimateDoomBuilder.CrossPlatform.sln
```

## Running

```bash
# Run the Avalonia application
dotnet run --project Source.CrossPlatform/Core/UltimateDoomBuilder.Avalonia/UltimateDoomBuilder.Avalonia.csproj
```

## Migration Status

### Phase 1: Foundation & Testing Infrastructure ✅
- [x] Create .NET 8 solution structure
- [x] Set up xUnit test projects
- [x] Create basic test infrastructure
- [x] Basic CI/CD configuration ready

### Phase 2: Core Architecture Migration 🚧
- [x] Create Avalonia application shell (no XAML)
- [x] Create OpenGLControlBase custom control
- [ ] Port core non-UI classes to .NET 8
- [ ] Implement cross-platform file I/O
- [ ] Implement cross-platform native interop

### Phase 3: Main Window & Key Forms 📋
- [ ] Complete MainForm migration
- [ ] Port RenderTargetControl functionality
- [ ] Migrate essential dialog forms
- [ ] Implement menu system

### Phase 4: Editor Forms Migration 📋
- [ ] Port editing forms
- [ ] Migrate browser forms
- [ ] Port configuration forms

### Phase 5: Plugin System 📋
- [ ] Update plugin interface
- [ ] Migrate plugins

### Phase 6: Testing & Validation 📋
- [ ] Comprehensive unit tests
- [ ] UI automation tests
- [ ] Cross-platform testing

## Key Components

### OpenGLControlBase

A custom Avalonia control that provides cross-platform OpenGL rendering capabilities:

```csharp
public abstract class OpenGLControlBase : Control
{
    protected abstract void OnOpenGLInit();
    protected abstract void OnOpenGLRender();
    protected virtual void OnOpenGLCleanup();
}
```

Usage:
```csharp
public class DoomRendererControl : OpenGLControlBase
{
    protected override void OnOpenGLInit()
    {
        // Initialize OpenGL context, shaders, etc.
    }
    
    protected override void OnOpenGLRender()
    {
        // Render frame
    }
}
```

### Code-Behind UI (No XAML)

Windows are created entirely in C#:

```csharp
public class MainWindow : Window
{
    private void InitializeWindow()
    {
        Title = "Ultimate Doom Builder";
        Width = 1200;
        Height = 800;
        
        var panel = new DockPanel();
        // Add controls programmatically
        Content = panel;
    }
}
```

## Testing

### Unit Tests

```bash
dotnet test Tests/UltimateDoomBuilder.Core.Tests/
```

### UI Tests (Future)

UI automation tests will use Avalonia's testing framework for end-to-end testing.

## Contributing

When contributing to the cross-platform migration:

1. Maintain the non-MVVM architecture
2. Do not use XAML - all UI should be in C# code-behind
3. Ensure .NET 8 compatibility
4. Add tests for new functionality
5. Test on multiple platforms (Windows, Linux, macOS)

## License

Same as the original Ultimate Doom Builder - GNU General Public License v3.0
