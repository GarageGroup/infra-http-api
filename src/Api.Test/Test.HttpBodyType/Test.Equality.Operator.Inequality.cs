using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpBodyTypeTest
{
    [Theory]
    [MemberData(nameof(EqualTestData))]
    public static void InequalityOperator_SourceIsEqualToOther_ExpectFalse(
        HttpBodyType source, HttpBodyType other)
    {
        var result = source != other;
        Assert.False(result);
    }

    [Theory]
    [MemberData(nameof(UnequalTestData))]
    public static void InequalityOperator_SourceIsNotEqualToOther_ExpectTrue(
        HttpBodyType source, HttpBodyType other)
    {
        var result = source != other;
        Assert.True(result);
    }
}
