using Xunit;
using UltimateDoomBuilder.Core.Map;
using UltimateDoomBuilder.Core.Geometry;
using UltimateDoomBuilder.Core.Types;

namespace UltimateDoomBuilder.Core.Tests;

/// <summary>
/// Tests for Map data structures
/// </summary>
public class MapDataTests
{
    [Fact]
    public void Vertex_Constructor_ShouldInitializeCorrectly()
    {
        var vertex = new Vertex(10.0, 20.0);
        
        Assert.Equal(10.0, vertex.Position.x);
        Assert.Equal(20.0, vertex.Position.y);
        Assert.Equal(MapElementType.VERTEX, vertex.ElementType);
    }

    [Fact]
    public void Vertex_Move_ShouldUpdatePosition()
    {
        var vertex = new Vertex(10.0, 20.0);
        vertex.Move(30.0, 40.0);
        
        Assert.Equal(30.0, vertex.Position.x);
        Assert.Equal(40.0, vertex.Position.y);
    }

    [Fact]
    public void Vertex_NaNCoordinates_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => new Vertex(double.NaN, 10.0));
        Assert.Throws<ArgumentException>(() => new Vertex(10.0, double.NaN));
    }

    [Fact]
    public void Vertex_ZHeight_ShouldStoreValues()
    {
        var vertex = new Vertex(0.0, 0.0);
        vertex.ZFloor = 100.0;
        vertex.ZCeiling = 200.0;
        
        Assert.Equal(100.0, vertex.ZFloor);
        Assert.Equal(200.0, vertex.ZCeiling);
    }

    [Fact]
    public void UniValue_Constructor_ShouldAcceptValidTypes()
    {
        var intValue = new UniValue(UniversalType.Integer, 42);
        var floatValue = new UniValue(UniversalType.Float, 3.14);
        var stringValue = new UniValue(UniversalType.String, "test");
        var boolValue = new UniValue(UniversalType.Boolean, true);
        
        Assert.Equal(42, intValue.Value);
        Assert.Equal(3.14, floatValue.Value);
        Assert.Equal("test", stringValue.Value);
        Assert.Equal(true, boolValue.Value);
    }

    [Fact]
    public void UniValue_InvalidType_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => new UniValue(UniversalType.Integer, new object()));
        Assert.Throws<ArgumentException>(() => new UniValue(UniversalType.Integer, null!));
    }

    [Fact]
    public void UniFields_GetSetInteger_ShouldWork()
    {
        var fields = new UniFields();
        UniFields.SetInteger(fields, "testkey", 123);
        
        var value = UniFields.GetInteger(fields, "testkey");
        Assert.Equal(123, value);
    }

    [Fact]
    public void UniFields_GetSetFloat_ShouldWork()
    {
        var fields = new UniFields();
        UniFields.SetFloat(fields, "testkey", 123.45);
        
        var value = UniFields.GetFloat(fields, "testkey");
        Assert.Equal(123.45, value);
    }

    [Fact]
    public void UniFields_GetSetString_ShouldWork()
    {
        var fields = new UniFields();
        UniFields.SetString(fields, "testkey", "hello");
        
        var value = UniFields.GetString(fields, "testkey");
        Assert.Equal("hello", value);
    }

    [Fact]
    public void UniFields_GetSetBoolean_ShouldWork()
    {
        var fields = new UniFields();
        UniFields.SetBoolean(fields, "testkey", true);
        
        var value = UniFields.GetBoolean(fields, "testkey");
        Assert.True(value);
    }

    [Fact]
    public void UniFields_DefaultValue_ShouldNotBeSaved()
    {
        var fields = new UniFields();
        UniFields.SetInteger(fields, "testkey", 0); // 0 is default
        
        Assert.False(fields.ContainsKey("testkey"));
    }

    [Fact]
    public void UniFields_GetMissingKey_ShouldReturnDefault()
    {
        var fields = new UniFields();
        
        var intValue = UniFields.GetInteger(fields, "missing");
        var floatValue = UniFields.GetFloat(fields, "missing");
        var stringValue = UniFields.GetString(fields, "missing");
        var boolValue = UniFields.GetBoolean(fields, "missing");
        
        Assert.Equal(0, intValue);
        Assert.Equal(0.0, floatValue);
        Assert.Equal("", stringValue);
        Assert.False(boolValue);
    }

    [Fact]
    public void MapElement_ShouldHaveUniqueHashCodes()
    {
        var vertex1 = new Vertex(0, 0);
        var vertex2 = new Vertex(0, 0);
        
        Assert.NotEqual(vertex1.GetHashCode(), vertex2.GetHashCode());
    }

    [Fact]
    public void MapElement_Fields_ShouldBeAccessible()
    {
        var vertex = new Vertex(0, 0);
        
        Assert.NotNull(vertex.Fields);
        Assert.Empty(vertex.Fields);
    }
}
