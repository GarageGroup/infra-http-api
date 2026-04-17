using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpBodyTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void EqualsWithObject_SourceIsEqualToOther_ExpectTrue(
        HttpBody source, HttpBody other)
    {
        var result = source.Equals((object)other);
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void EqualsWithObject_SourceIsNotEqualToOther_ExpectFalse(
        HttpBody source, HttpBody other)
    {
        var result = source.Equals((object)other);
        Assert.False(result);
    }

    [Fact]
    public static void EqualsWithObject_OtherIsNull_ExpectFalse()
    {
        object? other = null;

        var result = default(HttpBody).Equals(other);
        Assert.False(result);
    }

    [Fact]
    public static void EqualsWithObject_OtherHasAnotherType_ExpectFalse()
    {
        var result = default(HttpBody).Equals(new object());
        Assert.False(result);
    }
}
