using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpSendInTest
{
    [Fact]
    public static void EqualsWithObject_ObjectIsNull_ExpectFalse()
    {
        var source = new HttpSendIn(HttpVerb.Get, "https://www.example.com/about");
        object? @object = null;

        var result = source.Equals(@object);
        Assert.False(result);
    }

    [Fact]
    public static void EqualsWithObject_ObjectIsNotHttpSendIn_ExpectFalse()
    {
        var source = new HttpSendIn(HttpVerb.Get, "https://www.example.com/products");
        var @object = new object();

        var result = source.Equals(@object);
        Assert.False(result);
    }

    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void EqualsWithObject_ObjectIsEqualToSource_ExpectTrue(
        HttpSendIn source, object @object)
    {
        var result = source.Equals(@object);
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void EqualsWithObject_ObjectIsNotEqualToSource_ExpectFalse(
        HttpSendIn source, object @object)
    {
        var result = source.Equals(@object);
        Assert.False(result);
    }
}