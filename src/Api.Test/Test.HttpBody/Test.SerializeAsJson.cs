using System.Text.Json;
using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpBodyTest
{
    [Fact]
    public static void SerializeAsJson_ExpectJsonTypeAndContent()
    {
        var source = SerializeModel.Create();

        var result = HttpBody.SerializeAsJson(source);

        Assert.Equal("application/json", result.Type.MediaType);
        Assert.Equal("utf-8", result.Type.CharSet);
        Assert.NotNull(result.Content);

        var json = result.Content?.ToString();
        Assert.Contains("\"firstName\":\"John\"", json);
    }

    [Fact]
    public static void SerializeAsJson_CustomSerializerOptions_ExpectAppliedOptions()
    {
        var source = SerializeModel.Create();
        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = null
        };

        var result = HttpBody.SerializeAsJson(source, serializerOptions);

        var json = result.Content?.ToString();
        Assert.Contains("\"FirstName\":\"John\"", json);
    }

    private sealed record class SerializeModel
    {
        public string? FirstName { get; init; }

        public int Age { get; init; }

        public static SerializeModel Create()
            =>
            new()
            {
                FirstName = "John",
                Age = 18
            };
    }
}
