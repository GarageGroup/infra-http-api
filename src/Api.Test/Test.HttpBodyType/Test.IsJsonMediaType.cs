using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpBodyTypeTest
{
    [Fact]
    public static void IsJsonMediaType_MediaTypeIsNull_ExpectFalse()
    {
        var source = new HttpBodyType(mediaType: null!, charSet: null!);

        var result = source.IsJsonMediaType(isApplicationJsonStrict: true);
        Assert.False(result);
    }

    [Fact]
    public static void IsJsonMediaType_MediaTypeIsApplicationJson_ExpectTrue()
    {
        var source = new HttpBodyType(mediaType: "APPLICATION/JSON", charSet: "utf-8");

        var result = source.IsJsonMediaType(isApplicationJsonStrict: true);
        Assert.True(result);
    }

    [Fact]
    public static void IsJsonMediaType_MediaTypeContainsJsonAndStrictIsFalse_ExpectTrue()
    {
        var source = new HttpBodyType(mediaType: "application/problem+json", charSet: "utf-8");

        var result = source.IsJsonMediaType(isApplicationJsonStrict: false);
        Assert.True(result);
    }

    [Fact]
    public static void IsJsonMediaType_MediaTypeContainsJsonAndStrictIsTrue_ExpectFalse()
    {
        var source = new HttpBodyType(mediaType: "application/problem+json", charSet: "utf-8");

        var result = source.IsJsonMediaType(isApplicationJsonStrict: true);
        Assert.False(result);
    }
}
