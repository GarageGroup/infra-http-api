using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpBodyTypeTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void EqualityOperator_SourceIsEqualToOther_ExpectTrue(
        HttpBodyType source, HttpBodyType other)
    {
        var result = source == other;
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void EqualityOperator_SourceIsNotEqualToOther_ExpectFalse(
        HttpBodyType source, HttpBodyType other)
    {
        var result = source == other;
        Assert.False(result);
    }
}
