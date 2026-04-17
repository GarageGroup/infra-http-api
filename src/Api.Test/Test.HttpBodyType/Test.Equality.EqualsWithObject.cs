using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpBodyTypeTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void EqualsWithObject_SourceIsEqualToOther_ExpectTrue(
        HttpBodyType source, HttpBodyType other)
    {
        var result = source.Equals((object)other);
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void EqualsWithObject_SourceIsNotEqualToOther_ExpectFalse(
        HttpBodyType source, HttpBodyType other)
    {
        var result = source.Equals((object)other);
        Assert.False(result);
    }

    [Fact]
    public static void EqualsWithObject_ObjectIsNull_ExpectFalse()
    {
        object? other = null;

        var result = default(HttpBodyType).Equals(other);
        Assert.False(result);
    }

    [Fact]
    public static void EqualsWithObject_ObjectHasAnotherType_ExpectFalse()
    {
        var result = default(HttpBodyType).Equals(new object());
        Assert.False(result);
    }
}
