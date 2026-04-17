using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpBodyTest
{
    [Fact]
    public static void DeserializeFromJson_ContentIsNull_ExpectDefault()
    {
        var source = new HttpBody
        {
            Type = new("application/json", "utf-8")
        };

        var result = source.DeserializeFromJson<DeserializeModel>();
        Assert.Null(result);
    }

    [Fact]
    public static void DeserializeFromJson_ExpectModel()
    {
        var source = new HttpBody
        {
            Type = new("application/json", "utf-8"),
            Content = new("{\"firstName\":\"John\",\"age\":18}")
        };

        var result = source.DeserializeFromJson<DeserializeModel>();

        Assert.NotNull(result);
        Assert.Equal("John", result?.FirstName);
        Assert.Equal(18, result?.Age);
    }

    [Fact]
    public static void DeserializeFromJson_CustomSerializerOptions_ExpectAppliedOptions()
    {
        var source = new HttpBody
        {
            Type = new("application/json", "utf-8"),
            Content = new("{\"firstName\":\"John\",\"age\":\"18\"}")
        };

        var serializerOptions = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNameCaseInsensitive = true
        };

        var result = source.DeserializeFromJson<DeserializeModel>(serializerOptions);

        Assert.NotNull(result);
        Assert.Equal("John", result?.FirstName);
        Assert.Equal(18, result?.Age);
    }

    private sealed record class DeserializeModel
    {
        public string? FirstName { get; init; }

        public int Age { get; init; }
    }
}
