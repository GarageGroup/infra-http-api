using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpVerbTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void EqualityOperator_SourceIsEqualToOther_ExpectTrue(
        HttpVerb source, HttpVerb other)
    {
        var result = source == other;
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void EqualityOperator_SourceIsNotEqualToOther_ExpectFalse(
        HttpVerb source, HttpVerb other)
    {
        var result = source == other;
        Assert.False(result);
    }

    [Theory]
    [MemberData(nameof(NullableEqualTestData))]
    public static void EqualityOperator_SourceIsNullEqualToOther_ExpectTrue(
        HttpVerb? source, HttpVerb? other)
    {
        var result = source == other;
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(NullableUnequalTestData))]
    public static void EqualityOperator_SourceIsNullNotEqualToOther_ExpectFalse(
        HttpVerb? source, HttpVerb? other)
    {
        var result = source == other;
        Assert.False(result);
    }
}
