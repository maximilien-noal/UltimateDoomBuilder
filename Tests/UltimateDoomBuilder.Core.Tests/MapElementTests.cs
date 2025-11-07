using Xunit;
using UltimateDoomBuilder.Core.Map;
using UltimateDoomBuilder.Core.Geometry;

namespace UltimateDoomBuilder.Core.Tests;

/// <summary>
/// Tests for additional Map elements (Thing, Sector)
/// </summary>
public class MapElementTests
{
    [Fact]
    public void Thing_Constructor_ShouldInitializeCorrectly()
    {
        var thing = new Thing(10.0, 20.0, 30.0, 1);
        
        Assert.Equal(10.0, thing.Position.x);
        Assert.Equal(20.0, thing.Position.y);
        Assert.Equal(30.0, thing.Position.z);
        Assert.Equal(1, thing.Type);
        Assert.Equal(MapElementType.THING, thing.ElementType);
    }

    [Fact]
    public void Thing_Move_ShouldUpdatePosition()
    {
        var thing = new Thing(0, 0, 0, 1);
        thing.Move(100, 200, 300);
        
        Assert.Equal(100.0, thing.Position.x);
        Assert.Equal(200.0, thing.Position.y);
        Assert.Equal(300.0, thing.Position.z);
    }

    [Fact]
    public void Thing_SetAngle_ShouldConvertToRadians()
    {
        var thing = new Thing(0, 0, 0, 1);
        thing.SetAngle(90); // 90 degrees
        
        Assert.Equal(90, thing.AngleDoom);
        // 90 degrees ≈ 1.5708 radians
        Assert.True(Math.Abs(thing.Angle - 1.5708) < 0.001);
    }

    [Fact]
    public void Thing_SetAngleRadians_ShouldConvertToDegrees()
    {
        var thing = new Thing(0, 0, 0, 1);
        thing.SetAngleRadians(Math.PI); // π radians = 180 degrees
        
        Assert.Equal(180, thing.AngleDoom);
        Assert.Equal(Math.PI, thing.Angle);
    }

    [Fact]
    public void Thing_Flags_ShouldStoreAndRetrieve()
    {
        var thing = new Thing(0, 0, 0, 1);
        
        thing.SetFlag("skill1", true);
        thing.SetFlag("skill2", false);
        
        Assert.True(thing.GetFlag("skill1"));
        Assert.False(thing.GetFlag("skill2"));
        Assert.False(thing.GetFlag("nonexistent"));
    }

    [Fact]
    public void Thing_Args_ShouldHaveFiveElements()
    {
        var thing = new Thing(0, 0, 0, 1);
        
        Assert.Equal(Thing.NUM_ARGS, thing.Args.Length);
        Assert.Equal(5, thing.Args.Length);
    }

    [Fact]
    public void Thing_Scale_ShouldDefaultToOne()
    {
        var thing = new Thing(0, 0, 0, 1);
        
        Assert.Equal(1.0, thing.ScaleX);
        Assert.Equal(1.0, thing.ScaleY);
    }

    [Fact]
    public void Thing_PitchRoll_ShouldConvertToRadians()
    {
        var thing = new Thing(0, 0, 0, 1);
        
        thing.SetPitch(45);
        thing.SetRoll(30);
        
        Assert.Equal(45, thing.Pitch);
        Assert.Equal(30, thing.Roll);
        Assert.True(Math.Abs(thing.PitchRad - 0.7854) < 0.001); // 45° ≈ 0.7854 rad
        Assert.True(Math.Abs(thing.RollRad - 0.5236) < 0.001);  // 30° ≈ 0.5236 rad
    }

    [Fact]
    public void Sector_Constructor_ShouldInitializeDefaults()
    {
        var sector = new Sector();
        
        Assert.Equal(0, sector.FloorHeight);
        Assert.Equal(128, sector.CeilingHeight);
        Assert.Equal("-", sector.FloorTexture);
        Assert.Equal("-", sector.CeilingTexture);
        Assert.Equal(0, sector.Effect);
        Assert.Equal(0, sector.Tag);
        Assert.Equal(160, sector.Brightness);
        Assert.Equal(MapElementType.SECTOR, sector.ElementType);
    }

    [Fact]
    public void Sector_Constructor_WithHeights_ShouldInitialize()
    {
        var sector = new Sector(50, 200);
        
        Assert.Equal(50, sector.FloorHeight);
        Assert.Equal(200, sector.CeilingHeight);
    }

    [Fact]
    public void Sector_Properties_ShouldUpdate()
    {
        var sector = new Sector();
        
        sector.FloorHeight = 100;
        sector.CeilingHeight = 300;
        sector.FloorTexture = "FLAT1";
        sector.CeilingTexture = "FLAT2";
        sector.Effect = 1;
        sector.Tag = 10;
        sector.Brightness = 200;
        
        Assert.Equal(100, sector.FloorHeight);
        Assert.Equal(300, sector.CeilingHeight);
        Assert.Equal("FLAT1", sector.FloorTexture);
        Assert.Equal("FLAT2", sector.CeilingTexture);
        Assert.Equal(1, sector.Effect);
        Assert.Equal(10, sector.Tag);
        Assert.Equal(200, sector.Brightness);
    }

    [Fact]
    public void Sector_Brightness_ShouldClamp()
    {
        var sector = new Sector();
        
        sector.Brightness = 300; // Above max
        Assert.Equal(255, sector.Brightness);
        
        sector.Brightness = -10; // Below min
        Assert.Equal(0, sector.Brightness);
    }

    [Fact]
    public void Sector_NullTexture_ShouldBecomeDash()
    {
        var sector = new Sector();
        
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type
        sector.FloorTexture = null;
        sector.CeilingTexture = null;
#pragma warning restore CS8625
        
        Assert.Equal("-", sector.FloorTexture);
        Assert.Equal("-", sector.CeilingTexture);
    }
}
