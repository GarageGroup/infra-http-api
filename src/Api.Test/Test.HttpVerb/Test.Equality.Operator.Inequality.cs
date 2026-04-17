using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpVerbTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void InequalityOperator_SourceIsEqualToOther_ExpectFalse(
        HttpVerb source, HttpVerb other)
    {
        var result = source != other;
        Assert.False(result);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void InequalityOperator_SourceIsNotEqualToOther_ExpectTrue(
        HttpVerb source, HttpVerb other)
    {
        var result = source != other;
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(NullableEqualTestData))]
    public static void InequalityOperator_SourceIsNullEqualToOther_ExpectFalse(
        HttpVerb? source, HttpVerb? other)
    {
        var result = source != other;
        Assert.False(result);
    }

    [Theory]
    [MemberData(nameof(NullableUnequalTestData))]
    public static void InequalityOperator_SourceIsNullNotEqualToOther_ExpectTrue(
        HttpVerb? source, HttpVerb? other)
    {
        var result = source != other;
        Assert.True(result);
    }
}
