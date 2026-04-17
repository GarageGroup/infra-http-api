using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpBodyTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void InequalityOperator_SourceIsEqualToOther_ExpectFalse(
        HttpBody source, HttpBody other)
    {
        var result = source != other;
        Assert.False(result);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void InequalityOperator_SourceIsNotEqualToOther_ExpectTrue(
        HttpBody source, HttpBody other)
    {
        var result = source != other;
        Assert.True(result);
    }
}
