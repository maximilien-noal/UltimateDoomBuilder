using Xunit;
using UltimateDoomBuilder.Core.Map;
using UltimateDoomBuilder.Core.Geometry;

namespace UltimateDoomBuilder.Core.Tests;

/// <summary>
/// Tests for Linedef and Sidedef map elements
/// </summary>
public class LinedefSidedefTests
{
    [Fact]
    public void Linedef_Constructor_ShouldInitializeCorrectly()
    {
        var v1 = new Vertex(0, 0);
        var v2 = new Vertex(100, 0);
        var linedef = new Linedef(v1, v2);
        
        Assert.Equal(v1, linedef.Start);
        Assert.Equal(v2, linedef.End);
        Assert.Equal(MapElementType.LINEDEF, linedef.ElementType);
        Assert.Null(linedef.Front);
        Assert.Null(linedef.Back);
    }

    [Fact]
    public void Linedef_Constructor_NullVertex_ShouldThrow()
    {
        var v1 = new Vertex(0, 0);
        
        Assert.Throws<ArgumentNullException>(() => new Linedef(null!, v1));
        Assert.Throws<ArgumentNullException>(() => new Linedef(v1, null!));
    }

    [Fact]
    public void Linedef_Length_ShouldCalculateCorrectly()
    {
        var v1 = new Vertex(0, 0);
        var v2 = new Vertex(30, 40);
        var linedef = new Linedef(v1, v2);
        
        // Distance = sqrt(30^2 + 40^2) = sqrt(900 + 1600) = sqrt(2500) = 50
        Assert.Equal(50.0, linedef.Length, 5);
        Assert.Equal(2500.0, linedef.LengthSq, 5);
    }

    [Fact]
    public void Linedef_Angle_ShouldCalculateCorrectly()
    {
        var v1 = new Vertex(0, 0);
        var v2 = new Vertex(1, 0); // Horizontal line to the right
        var linedef = new Linedef(v1, v2);
        
        double angle = linedef.Angle;
        // Horizontal line pointing right
        Assert.True(Math.Abs(angle - Angle2D.PIHALF) < 0.001 || Math.Abs(angle) < 0.001);
    }

    [Fact]
    public void Linedef_Line_ShouldReturnLine2D()
    {
        var v1 = new Vertex(10, 20);
        var v2 = new Vertex(30, 40);
        var linedef = new Linedef(v1, v2);
        
        var line = linedef.Line;
        Assert.Equal(10.0, line.v1.x);
        Assert.Equal(20.0, line.v1.y);
        Assert.Equal(30.0, line.v2.x);
        Assert.Equal(40.0, line.v2.y);
    }

    [Fact]
    public void Linedef_SetSidedefs_ShouldWork()
    {
        var v1 = new Vertex(0, 0);
        var v2 = new Vertex(100, 0);
        var linedef = new Linedef(v1, v2);
        var sector = new Sector();
        
        var frontSide = new Sidedef(sector);
        var backSide = new Sidedef(sector);
        
        linedef.SetFront(frontSide);
        linedef.SetBack(backSide);
        
        Assert.Equal(frontSide, linedef.Front);
        Assert.Equal(backSide, linedef.Back);
    }

    [Fact]
    public void Linedef_IsSingleSided_ShouldDetect()
    {
        var v1 = new Vertex(0, 0);
        var v2 = new Vertex(100, 0);
        var linedef = new Linedef(v1, v2);
        var sector = new Sector();
        
        Assert.False(linedef.IsSingleSided());
        Assert.False(linedef.IsDoubleSided());
        
        linedef.SetFront(new Sidedef(sector));
        Assert.True(linedef.IsSingleSided());
        Assert.False(linedef.IsDoubleSided());
        
        linedef.SetBack(new Sidedef(sector));
        Assert.False(linedef.IsSingleSided());
        Assert.True(linedef.IsDoubleSided());
    }

    [Fact]
    public void Linedef_Flags_ShouldStoreAndRetrieve()
    {
        var v1 = new Vertex(0, 0);
        var v2 = new Vertex(100, 0);
        var linedef = new Linedef(v1, v2);
        
        linedef.SetFlag("blocking", true);
        linedef.SetFlag("twosided", false);
        
        Assert.True(linedef.GetFlag("blocking"));
        Assert.False(linedef.GetFlag("twosided"));
        Assert.False(linedef.GetFlag("nonexistent"));
    }

    [Fact]
    public void Linedef_Args_ShouldHaveFiveElements()
    {
        var v1 = new Vertex(0, 0);
        var v2 = new Vertex(100, 0);
        var linedef = new Linedef(v1, v2);
        
        Assert.Equal(Linedef.NUM_ARGS, linedef.Args.Length);
        Assert.Equal(5, linedef.Args.Length);
    }

    [Fact]
    public void Linedef_Tags_ShouldWork()
    {
        var v1 = new Vertex(0, 0);
        var v2 = new Vertex(100, 0);
        var linedef = new Linedef(v1, v2);
        
        linedef.Tag = 10;
        Assert.Equal(10, linedef.Tag);
        Assert.Equal(10, linedef.Tags[0]);
        
        linedef.Tags = new System.Collections.Generic.List<int> { 1, 2, 3 };
        Assert.Equal(1, linedef.Tag);
        Assert.Equal(3, linedef.Tags.Count);
    }

    [Fact]
    public void Sidedef_Constructor_ShouldInitializeCorrectly()
    {
        var sector = new Sector();
        var sidedef = new Sidedef(sector);
        
        Assert.Equal(sector, sidedef.Sector);
        Assert.Equal(MapElementType.SIDEDEF, sidedef.ElementType);
        Assert.Equal("-", sidedef.HighTexture);
        Assert.Equal("-", sidedef.MiddleTexture);
        Assert.Equal("-", sidedef.LowTexture);
        Assert.Equal(0, sidedef.OffsetX);
        Assert.Equal(0, sidedef.OffsetY);
    }

    [Fact]
    public void Sidedef_Textures_ShouldSetAndGet()
    {
        var sector = new Sector();
        var sidedef = new Sidedef(sector);
        
        sidedef.SetTextureHigh("WALL1");
        sidedef.SetTextureMid("WALL2");
        sidedef.SetTextureLow("WALL3");
        
        Assert.Equal("WALL1", sidedef.HighTexture);
        Assert.Equal("WALL2", sidedef.MiddleTexture);
        Assert.Equal("WALL3", sidedef.LowTexture);
    }

    [Fact]
    public void Sidedef_NullTexture_ShouldBecomeDash()
    {
        var sector = new Sector();
        var sidedef = new Sidedef(sector);
        
        sidedef.SetTextureHigh(null);
        sidedef.SetTextureMid(null);
        sidedef.SetTextureLow(null);
        
        Assert.Equal("-", sidedef.HighTexture);
        Assert.Equal("-", sidedef.MiddleTexture);
        Assert.Equal("-", sidedef.LowTexture);
    }

    [Fact]
    public void Sidedef_Offsets_ShouldSetAndGet()
    {
        var sector = new Sector();
        var sidedef = new Sidedef(sector);
        
        sidedef.OffsetX = 16;
        sidedef.OffsetY = 32;
        
        Assert.Equal(16, sidedef.OffsetX);
        Assert.Equal(32, sidedef.OffsetY);
    }

    [Fact]
    public void Sidedef_IsFront_ShouldDetectCorrectly()
    {
        var v1 = new Vertex(0, 0);
        var v2 = new Vertex(100, 0);
        var linedef = new Linedef(v1, v2);
        var sector = new Sector();
        
        var frontSide = new Sidedef(sector);
        var backSide = new Sidedef(sector);
        
        frontSide.SetLinedef(linedef);
        backSide.SetLinedef(linedef);
        linedef.SetFront(frontSide);
        linedef.SetBack(backSide);
        
        Assert.True(frontSide.IsFront);
        Assert.False(backSide.IsFront);
    }

    [Fact]
    public void Sidedef_Other_ShouldReturnOppositeSide()
    {
        var v1 = new Vertex(0, 0);
        var v2 = new Vertex(100, 0);
        var linedef = new Linedef(v1, v2);
        var sector = new Sector();
        
        var frontSide = new Sidedef(sector);
        var backSide = new Sidedef(sector);
        
        frontSide.SetLinedef(linedef);
        backSide.SetLinedef(linedef);
        linedef.SetFront(frontSide);
        linedef.SetBack(backSide);
        
        Assert.Equal(backSide, frontSide.Other);
        Assert.Equal(frontSide, backSide.Other);
    }

    [Fact]
    public void Sidedef_Flags_ShouldStoreAndRetrieve()
    {
        var sector = new Sector();
        var sidedef = new Sidedef(sector);
        
        sidedef.SetFlag("dontpegtop", true);
        sidedef.SetFlag("dontpegbottom", false);
        
        Assert.True(sidedef.GetFlag("dontpegtop"));
        Assert.False(sidedef.GetFlag("dontpegbottom"));
        Assert.False(sidedef.GetFlag("nonexistent"));
    }
}
