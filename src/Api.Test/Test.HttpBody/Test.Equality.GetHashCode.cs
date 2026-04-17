using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpBodyTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void GetHashCode_SourceIsEqualToOther_ExpectEqualHashCodes(
        HttpBody source, HttpBody other)
    {
        var sourceHashCode = source.GetHashCode();
        var otherHashCode = other.GetHashCode();

        Assert.Equal(sourceHashCode, otherHashCode);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void GetHashCode_SourceIsNotEqualToOther_ExpectUnequalHashCodes(
        HttpBody source, HttpBody other)
    {
        var sourceHashCode = source.GetHashCode();
        var otherHashCode = other.GetHashCode();

        Assert.NotEqual(sourceHashCode, otherHashCode);
    }
}
