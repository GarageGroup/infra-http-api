using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpBodyTypeTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void GetHashCode_SourceIsEqualToOther_ExpectEqualHashCodes(
        HttpBodyType source, HttpBodyType other)
    {
        var sourceHashCode = source.GetHashCode();
        var otherHashCode = other.GetHashCode();

        Assert.Equal(sourceHashCode, otherHashCode);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void GetHashCode_SourceIsNotEqualToOther_ExpectUnequalHashCodes(
        HttpBodyType source, HttpBodyType other)
    {
        var sourceHashCode = source.GetHashCode();
        var otherHashCode = other.GetHashCode();

        Assert.NotEqual(sourceHashCode, otherHashCode);
    }
}
