using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpSendInTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void InequalityOperator_SourceIsEqualToOther_ExpectFalse(
        HttpSendIn source, HttpSendIn other)
    {
        var result = source != other;
        Assert.False(result);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void InequalityOperator_SourceIsNotEqualToOther_ExpectTrue(
        HttpSendIn source, HttpSendIn other)
    {
        var result = source != other;
        Assert.True(result);
    }
}