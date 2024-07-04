using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpSendInTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void EqualityOperator_SourceIsEqualToOther_ExpectTrue(
        HttpSendIn source, HttpSendIn other)
    {
        var result = source == other;
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void EqualityOperator_SourceIsNotEqualToOther_ExpectFalse(
        HttpSendIn source, HttpSendIn other)
    {
        var result = source == other;
        Assert.False(result);
    }
}