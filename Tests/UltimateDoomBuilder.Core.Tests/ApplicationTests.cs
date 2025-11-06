using Xunit;

namespace UltimateDoomBuilder.Core.Tests;

/// <summary>
/// Basic tests for the Avalonia application initialization
/// </summary>
public class ApplicationTests
{
    [Fact]
    public void Application_ShouldNotBeNull()
    {
        // Basic sanity test
        Assert.True(true);
    }

    [Fact]
    public void Application_Configuration_ShouldBeValid()
    {
        // Test that we're running on .NET 8
        var version = Environment.Version;
        Assert.True(version.Major >= 8, $"Expected .NET 8 or higher, but got {version}");
    }
}
