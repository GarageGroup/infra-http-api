using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpBodyTypeTest
{
    [Fact]
    public static void Constructor_MediaTypeIsWhiteSpace_ExpectMediaTypeNull()
    {
        var result = new HttpBodyType(mediaType: " \n\r ", charSet: "utf-8");

        Assert.Null(result.MediaType);
        Assert.Equal("utf-8", result.CharSet);
    }

    [Fact]
    public static void Constructor_CharSetIsWhiteSpace_ExpectCharSetNull()
    {
        var result = new HttpBodyType(mediaType: "application/json", charSet: " ");

        Assert.Equal("application/json", result.MediaType);
        Assert.Null(result.CharSet);
    }
}
