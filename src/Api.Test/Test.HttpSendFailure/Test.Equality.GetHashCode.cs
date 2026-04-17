using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpSendFailureTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void GetHashCode_SourceIsEqualToOther_ExpectEqualHashCodes(
        HttpSendFailure source, HttpSendFailure other)
    {
        var sourceHashCode = source.GetHashCode();
        var otherHashCode = other.GetHashCode();

        Assert.Equal(sourceHashCode, otherHashCode);
    }
}
