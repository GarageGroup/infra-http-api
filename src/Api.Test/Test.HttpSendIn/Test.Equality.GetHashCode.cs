using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpSendInTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void GetHashCode_SourceIsEqualToOther_ExpectEqualHashCodes(
        HttpSendIn source, HttpSendIn other)
    {
        var sourceHashCode = source.GetHashCode();
        var otherHashCode = other.GetHashCode();

        Assert.StrictEqual(sourceHashCode, otherHashCode);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void GetHashCode_SourceIsNotEqualToOther_ExpectUnequalHashCodes(
        HttpSendIn source, HttpSendIn other)
    {
        var sourceHashCode = source.GetHashCode();
        var otherHashCode = other.GetHashCode();

        Assert.NotEqual(sourceHashCode, otherHashCode);
    }
}