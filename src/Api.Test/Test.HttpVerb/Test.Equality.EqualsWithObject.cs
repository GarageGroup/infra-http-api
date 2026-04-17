using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpVerbTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void EqualsWithObject_SourceIsEqualToOther_ExpectTrue(
        HttpVerb source, HttpVerb other)
    {
        var result = source.Equals((object)other);
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void EqualsWithObject_SourceIsNotEqualToOther_ExpectFalse(
        HttpVerb source, HttpVerb other)
    {
        var result = source.Equals((object)other);
        Assert.False(result);
    }

    [Fact]
    public static void EqualsWithObject_OtherHasAnotherType_ExpectFalse()
    {
        var result = HttpVerb.Get.Equals(new object());
        Assert.False(result);
    }
}
