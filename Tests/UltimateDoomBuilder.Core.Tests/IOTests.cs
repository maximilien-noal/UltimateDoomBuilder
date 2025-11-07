using Xunit;
using UltimateDoomBuilder.Core.IO;
using System.Text;

namespace UltimateDoomBuilder.Core.Tests;

/// <summary>
/// Tests for I/O classes
/// </summary>
public class IOTests
{
    [Fact]
    public void Lump_Constructor_ShouldInitializeCorrectly()
    {
        var lump = new Lump("THINGS", 100, 200);
        
        Assert.Equal("THINGS", lump.Name);
        Assert.Equal(100, lump.Offset);
        Assert.Equal(200, lump.Length);
    }

    [Fact]
    public void Lump_Name_ShouldBeUppercase()
    {
        var lump = new Lump("things", 0, 0);
        
        Assert.Equal("THINGS", lump.Name);
    }

    [Fact]
    public void Lump_MakeFixedName_ShouldPadToEightBytes()
    {
        var fixedName = Lump.MakeFixedName("THINGS");
        
        Assert.Equal(8, fixedName.Length);
        
        // First 6 bytes should be "THINGS"
        string name = Encoding.ASCII.GetString(fixedName, 0, 6);
        Assert.Equal("THINGS", name);
        
        // Remaining bytes should be null
        Assert.Equal(0, fixedName[6]);
        Assert.Equal(0, fixedName[7]);
    }

    [Fact]
    public void Lump_MakeFixedName_ShouldTruncateLongNames()
    {
        var fixedName = Lump.MakeFixedName("VERYLONGNAME");
        
        Assert.Equal(8, fixedName.Length);
        
        string name = Encoding.ASCII.GetString(fixedName);
        Assert.Equal("VERYLONG", name);
    }

    [Fact]
    public void Lump_MakeNormalName_ShouldConvertFromFixedName()
    {
        var fixedName = Lump.MakeFixedName("SECTORS");
        var normalName = Lump.MakeNormalName(fixedName);
        
        Assert.Equal("SECTORS", normalName);
    }

    [Fact]
    public void Lump_MakeNormalName_ShouldHandleNullBytes()
    {
        byte[] fixedName = new byte[8];
        Encoding.ASCII.GetBytes("MAP01").CopyTo(fixedName, 0);
        
        var normalName = Lump.MakeNormalName(fixedName);
        
        Assert.Equal("MAP01", normalName);
    }

    [Fact]
    public void Lump_IsValidMapLumpName_ShouldValidateCorrectly()
    {
        Assert.True(Lump.IsValidMapLumpName("THINGS"));
        Assert.True(Lump.IsValidMapLumpName("LINEDEFS"));
        Assert.True(Lump.IsValidMapLumpName("MAP01"));
        Assert.True(Lump.IsValidMapLumpName("E1M1"));
        
        Assert.False(Lump.IsValidMapLumpName(""));
        Assert.False(Lump.IsValidMapLumpName("TOOLONGNAME"));
        Assert.False(Lump.IsValidMapLumpName("MAP-01")); // Contains invalid char
        Assert.False(Lump.IsValidMapLumpName("map.wad")); // Contains invalid char
    }

    [Fact]
    public void Lump_Constructor_FromFixedName_ShouldWork()
    {
        byte[] fixedName = Lump.MakeFixedName("VERTEXES");
        var lump = new Lump(fixedName, 500, 1000);
        
        Assert.Equal("VERTEXES", lump.Name);
        Assert.Equal(500, lump.Offset);
        Assert.Equal(1000, lump.Length);
    }

    [Fact]
    public void Lump_FixedName_ShouldMatchConstructorName()
    {
        var lump = new Lump("SIDEDEFS", 0, 0);
        string reconstructedName = Lump.MakeNormalName(lump.FixedName);
        
        Assert.Equal("SIDEDEFS", reconstructedName);
    }

    [Fact]
    public void Lump_ToString_ShouldProvideReadableFormat()
    {
        var lump = new Lump("SECTORS", 1000, 500);
        string str = lump.ToString();
        
        Assert.Contains("SECTORS", str);
        Assert.Contains("500", str);
        Assert.Contains("1000", str);
    }

    [Fact]
    public void Lump_Dispose_ShouldMarkAsDisposed()
    {
        var lump = new Lump("TEST", 0, 0);
        
        Assert.False(lump.IsDisposed);
        
        lump.Dispose();
        
        Assert.True(lump.IsDisposed);
    }

    [Fact]
    public void Lump_EmptyName_ShouldBeHandled()
    {
        var fixedName = Lump.MakeFixedName("");
        Assert.Equal(8, fixedName.Length);
        
        var normalName = Lump.MakeNormalName(fixedName);
        Assert.Equal(string.Empty, normalName);
    }
}
