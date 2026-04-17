using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpSendFailureTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void EqualityOperator_SourceIsEqualToOther_ExpectTrue(
        HttpSendFailure source, HttpSendFailure other)
    {
        var result = source == other;
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void EqualityOperator_SourceIsNotEqualToOther_ExpectFalse(
        HttpSendFailure source, HttpSendFailure other)
    {
        var result = source == other;
        Assert.False(result);
    }
}
