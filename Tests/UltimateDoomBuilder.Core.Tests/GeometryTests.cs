using Xunit;
using UltimateDoomBuilder.Core.Geometry;

namespace UltimateDoomBuilder.Core.Tests;

/// <summary>
/// Tests for Vector2D geometry class
/// </summary>
public class GeometryTests
{
    [Fact]
    public void Vector2D_Constructor_ShouldInitializeCorrectly()
    {
        var vec = new Vector2D(3.0, 4.0);
        Assert.Equal(3.0, vec.x);
        Assert.Equal(4.0, vec.y);
    }

    [Fact]
    public void Vector2D_Addition_ShouldWork()
    {
        var a = new Vector2D(1.0, 2.0);
        var b = new Vector2D(3.0, 4.0);
        var result = a + b;
        
        Assert.Equal(4.0, result.x);
        Assert.Equal(6.0, result.y);
    }

    [Fact]
    public void Vector2D_Subtraction_ShouldWork()
    {
        var a = new Vector2D(5.0, 7.0);
        var b = new Vector2D(2.0, 3.0);
        var result = a - b;
        
        Assert.Equal(3.0, result.x);
        Assert.Equal(4.0, result.y);
    }

    [Fact]
    public void Vector2D_GetLength_ShouldCalculateCorrectly()
    {
        var vec = new Vector2D(3.0, 4.0);
        var length = vec.GetLength();
        
        Assert.Equal(5.0, length, 5); // 5 decimal places precision
    }

    [Fact]
    public void Vector2D_DotProduct_ShouldCalculateCorrectly()
    {
        var a = new Vector2D(1.0, 2.0);
        var b = new Vector2D(3.0, 4.0);
        var dotProduct = Vector2D.DotProduct(a, b);
        
        Assert.Equal(11.0, dotProduct); // 1*3 + 2*4 = 11
    }

    [Fact]
    public void Vector2D_GetNormal_ShouldNormalizeVector()
    {
        var vec = new Vector2D(3.0, 4.0);
        var normal = vec.GetNormal();
        var length = normal.GetLength();
        
        Assert.Equal(1.0, length, 5); // Normalized vector should have length 1
    }

    [Fact]
    public void Vector3D_Constructor_ShouldInitializeCorrectly()
    {
        var vec = new Vector3D(1.0, 2.0, 3.0);
        Assert.Equal(1.0, vec.x);
        Assert.Equal(2.0, vec.y);
        Assert.Equal(3.0, vec.z);
    }

    [Fact]
    public void Vector3D_Addition_ShouldWork()
    {
        var a = new Vector3D(1.0, 2.0, 3.0);
        var b = new Vector3D(4.0, 5.0, 6.0);
        var result = a + b;
        
        Assert.Equal(5.0, result.x);
        Assert.Equal(7.0, result.y);
        Assert.Equal(9.0, result.z);
    }

    [Fact]
    public void Angle2D_Constants_ShouldHaveCorrectValues()
    {
        // Test that basic constants are defined
        Assert.True(Angle2D.PI > 3.14 && Angle2D.PI < 3.15);
        Assert.True(Angle2D.PI2 > 6.28 && Angle2D.PI2 < 6.29);
    }
}
