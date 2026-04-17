using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpSendFailureTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void EqualsWithObject_SourceIsEqualToOther_ExpectTrue(
        HttpSendFailure source, HttpSendFailure other)
    {
        var result = source.Equals((object)other);
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void EqualsWithObject_SourceIsNotEqualToOther_ExpectFalse(
        HttpSendFailure source, HttpSendFailure other)
    {
        var result = source.Equals((object)other);
        Assert.False(result);
    }

    [Fact]
    public static void EqualsWithObject_ObjectIsNull_ExpectFalse()
    {
        object? other = null;

        var result = default(HttpSendFailure).Equals(other);
        Assert.False(result);
    }

    [Fact]
    public static void EqualsWithObject_ObjectHasAnotherType_ExpectFalse()
    {
        var result = default(HttpSendFailure).Equals(new object());
        Assert.False(result);
    }
}
