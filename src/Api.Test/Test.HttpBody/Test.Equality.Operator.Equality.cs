using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpBodyTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void EqualityOperator_SourceIsEqualToOther_ExpectTrue(
        HttpBody source, HttpBody other)
    {
        var result = source == other;
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void EqualityOperator_SourceIsNotEqualToOther_ExpectFalse(
        HttpBody source, HttpBody other)
    {
        var result = source == other;
        Assert.False(result);
    }
}
