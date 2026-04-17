using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpVerbTest
{
    [Theory]
    [InlineData("GET")]
    [InlineData("POST")]
    [InlineData("PATCH")]
    public static void ToString_ExpectName(string verbName)
    {
        var source = HttpVerb.From(verbName.ToLowerInvariant());

        var result = source.ToString();
        Assert.Equal(verbName, result);
    }
}
