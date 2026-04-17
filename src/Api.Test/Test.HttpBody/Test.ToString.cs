using System;
using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpBodyTest
{
    [Fact]
    public static void ToString_ContentIsNull_ExpectNull()
    {
        var source = new HttpBody
        {
            Type = new("application/json", "utf-8")
        };

        var result = source.ToString();
        Assert.Null(result);
    }

    [Fact]
    public static void ToString_ContentTypeIsEmpty_ExpectBodyOnly()
    {
        var source = new HttpBody
        {
            Type = default,
            Content = new("{\"value\":1}")
        };

        var result = source.ToString();
        Assert.Equal("{\"value\":1}", result);
    }

    [Fact]
    public static void ToString_ContentTypeHasMediaType_ExpectHeaderAndBody()
    {
        var source = new HttpBody
        {
            Type = new("application/json"),
            Content = new("{\"value\":1}")
        };

        var result = source.ToString();
        var expected = $"Content-Type: application/json{Environment.NewLine}{{\"value\":1}}";

        Assert.Equal(expected, result);
    }

    [Fact]
    public static void ToString_ContentTypeHasMediaTypeAndCharSet_ExpectHeaderAndBody()
    {
        var source = new HttpBody
        {
            Type = new("application/json", "utf-8"),
            Content = new("{\"value\":1}")
        };

        var result = source.ToString();
        var expected = $"Content-Type: application/json; charset=utf-8{Environment.NewLine}{{\"value\":1}}";

        Assert.Equal(expected, result);
    }
}
