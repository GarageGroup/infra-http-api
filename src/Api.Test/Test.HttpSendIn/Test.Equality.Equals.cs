using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpSendInTest
{
    [Fact]
    public static void Equals_OtherIsNull_ExpectFalse()
    {
        var source = new HttpSendIn(HttpVerb.Get, "https://www.example.com/about");
        var result = source.Equals(null);

        Assert.False(result);
    }

    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void Equals_SourceIsEqualToOther_ExpectTrue(
        HttpSendIn source, HttpSendIn other)
    {
        var result = source.Equals(other);
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void Equals_SourceIsNotEqualToOther_ExpectFalse(
        HttpSendIn source, HttpSendIn other)
    {
        var result = source.Equals(other);
        Assert.False(result);
    }
}