using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpSendFailureTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void InequalityOperator_SourceIsEqualToOther_ExpectFalse(
        HttpSendFailure source, HttpSendFailure other)
    {
        var result = source != other;
        Assert.False(result);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void InequalityOperator_SourceIsNotEqualToOther_ExpectTrue(
        HttpSendFailure source, HttpSendFailure other)
    {
        var result = source != other;
        Assert.True(result);
    }
}
