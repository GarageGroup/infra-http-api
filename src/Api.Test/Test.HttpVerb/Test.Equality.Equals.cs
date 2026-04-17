using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpVerbTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void Equals_SourceIsEqualToOther_ExpectTrue(
        HttpVerb source, HttpVerb other)
    {
        var result = source.Equals(other);
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void Equals_SourceIsNotEqualToOther_ExpectFalse(
        HttpVerb source, HttpVerb other)
    {
        var result = source.Equals(other);
        Assert.False(result);
    }

    [Fact]
    public static void Equals_OtherIsNull_ExpectFalse()
    {
        HttpVerb? other = null;

        var result = HttpVerb.Get.Equals(other);
        Assert.False(result);
    }
}
