using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpVerbTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void GetHashCode_SourceIsEqualToOther_ExpectEqualHashes(
        HttpVerb source, HttpVerb other)
    {
        var sourceHash = source.GetHashCode();
        var otherHash = other.GetHashCode();

        Assert.Equal(sourceHash, otherHash);
    }
}
