using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpBodyTypeTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void Equals_SourceIsEqualToOther_ExpectTrue(
        HttpBodyType source, HttpBodyType other)
    {
        var result = source.Equals(other);
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void Equals_SourceIsNotEqualToOther_ExpectFalse(
        HttpBodyType source, HttpBodyType other)
    {
        var result = source.Equals(other);
        Assert.False(result);
    }
}
