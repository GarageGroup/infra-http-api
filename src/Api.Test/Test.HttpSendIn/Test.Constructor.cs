using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpSendInTest
{
    [Fact]
    public static void Constructor_ExpectMethodAssigned()
    {
        var source = new HttpSendIn(HttpVerb.Post, "https://www.example.com/about");

        Assert.Equal(HttpVerb.Post, source.Method);
    }

    [Fact]
    public static void Constructor_RequestUriIsNull_ExpectEmptyString()
    {
        var source = new HttpSendIn(HttpVerb.Get, null!);

        Assert.Equal(string.Empty, source.RequestUri);
    }

    [Fact]
    public static void Constructor_RequestUriHasSurroundingWhiteSpace_ExpectTrimmed()
    {
        var source = new HttpSendIn(HttpVerb.Get, "  /api/items  ");

        Assert.Equal("/api/items", source.RequestUri);
    }
}
