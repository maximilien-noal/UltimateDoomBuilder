# Cross-Platform Migration Guide

This document provides an overview of the cross-platform migration of Ultimate Doom Builder from Windows Forms/.NET Framework to Avalonia UI/.NET 8.

## Quick Start

### Building the Cross-Platform Version

```bash
# Build the entire cross-platform solution
dotnet build UltimateDoomBuilder.CrossPlatform.sln

# Run tests
dotnet test

# Run the Avalonia application
dotnet run --project Source.CrossPlatform/Core/UltimateDoomBuilder.Avalonia/UltimateDoomBuilder.Avalonia.csproj
```

## Architecture Overview

### Design Principles

1. **No XAML**: All UI is built using C# code-behind, maintaining consistency with the original WinForms approach
2. **Non-MVVM**: Preserves the original architecture pattern for easier migration
3. **.NET 8 Only**: Modern runtime for maximum cross-platform compatibility
4. **OpenGL Rendering**: Custom `OpenGLControlBase` for cross-platform 3D rendering

### Project Structure

```
UltimateDoomBuilder/
├── Source/                                    # Original WinForms source
├── Source.CrossPlatform/                      # New cross-platform source
│   └── Core/
│       ├── UltimateDoomBuilder.Core/         # Core library (geometry, data, etc.)
│       └── UltimateDoomBuilder.Avalonia/     # Avalonia UI application
├── Tests/                                     # Test projects
│   └── UltimateDoomBuilder.Core.Tests/       # xUnit tests
├── UltimateDoomBuilder.CrossPlatform.sln     # .NET 8 solution
└── CROSSPLATFORM_MIGRATION.md                # Detailed migration docs
```

## Key Components

### OpenGLControlBase

Custom Avalonia control for cross-platform OpenGL rendering:

```csharp
public abstract class OpenGLControlBase : Control
{
    protected abstract void OnOpenGLInit();    // Initialize OpenGL
    protected abstract void OnOpenGLRender();   // Render frame
    protected virtual void OnOpenGLCleanup();   // Cleanup resources
}
```

### Core Library

Platform-independent classes:
- **Geometry**: Vector2D, Vector3D, Angle2D, Line2D, etc.
- **Data Structures**: Map, Thing, Linedef, Sector, etc. (to be ported)
- **I/O**: Cross-platform file operations (to be ported)

### UI Layer

Avalonia-based user interface built entirely in C#:
- No XAML files
- Code-behind UI construction
- Event-driven architecture matching original

## Migration Progress

### Completed ✅

- [x] .NET 8 solution structure
- [x] xUnit test framework (14 tests passing)
- [x] Avalonia application shell (no XAML)
- [x] OpenGLControlBase for rendering
- [x] Core library with geometry classes
- [x] Comprehensive documentation

### In Progress 🚧

- [ ] Core data structures (Map, Thing, Linedef, etc.)
- [ ] Cross-platform file I/O
- [ ] Configuration system
- [ ] Rendering pipeline integration

### Planned 📋

- [ ] Complete MainForm UI
- [ ] Editor forms (Linedef, Sector, Thing editors)
- [ ] Browser forms (Effect, Thing, Flat browsers)
- [ ] Plugin system migration
- [ ] Comprehensive testing

## Development Workflow

### Running Tests

```bash
# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --logger "console;verbosity=detailed"

# Run specific test class
dotnet test --filter GeometryTests
```

### Building

```bash
# Debug build
dotnet build

# Release build
dotnet build -c Release

# Build specific project
dotnet build Source.CrossPlatform/Core/UltimateDoomBuilder.Core/UltimateDoomBuilder.Core.csproj
```

### IDE Support

- **Visual Studio 2022+**: Full support for .NET 8 and Avalonia
- **Visual Studio Code**: With C# and .NET extensions
- **JetBrains Rider**: Full support

## Platform-Specific Considerations

### Windows

- Uses native Win32 OpenGL implementation
- Full feature parity with original version

### Linux

- Requires OpenGL 3.2+ support
- Uses X11 or Wayland
- Native file dialogs via system integration

### macOS

- Uses Metal backend for rendering (via Avalonia)
- Native macOS controls and dialogs
- Requires macOS 10.15+ (Catalina or later)

## Testing Strategy

### Unit Tests

Focus on pure logic and algorithms:
- Geometry calculations
- Data structure operations
- File parsing
- Configuration management

### Integration Tests

Test cross-component functionality:
- Map loading and saving
- Rendering pipeline
- Plugin loading

### UI Tests

Automated UI testing using Avalonia test framework:
- Form interactions
- Menu operations
- Toolbar actions

## Contributing

When contributing to the migration:

1. **Preserve Architecture**: Maintain non-MVVM pattern
2. **No XAML**: Build UI in C# code-behind only
3. **Add Tests**: Write tests for new functionality
4. **Update Documentation**: Keep docs in sync
5. **Cross-Platform**: Test on multiple platforms

## Resources

- [Avalonia Documentation](https://docs.avaloniaui.net/)
- [.NET 8 Documentation](https://docs.microsoft.com/dotnet/)
- [Original UDB Documentation](README.md)
- [Migration Details](CROSSPLATFORM_MIGRATION.md)

## License

Same as original Ultimate Doom Builder: GNU General Public License v3.0

---

For more detailed information about the migration process, see [CROSSPLATFORM_MIGRATION.md](CROSSPLATFORM_MIGRATION.md).
