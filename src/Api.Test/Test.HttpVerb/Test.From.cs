using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpVerbTest
{
    [Fact]
    public static void From_NameIsNull_ExpectNameIsGet()
    {
        var result = HttpVerb.From(null!);
        Assert.Equal(HttpVerb.Get.Name, result.Name);
    }

    [Fact]
    public static void From_NameIsWhiteSpace_ExpectNameIsGet()
    {
        var result = HttpVerb.From(" \n\r ");
        Assert.Equal(HttpVerb.Get.Name, result.Name);
    }

    [Fact]
    public static void From_NameHasLowerCase_ExpectNameIsUpperCase()
    {
        var result = HttpVerb.From("test");
        Assert.Equal("TEST", result.Name);
    }
}
