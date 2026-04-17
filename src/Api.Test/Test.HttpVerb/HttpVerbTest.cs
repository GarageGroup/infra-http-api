using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

public static partial class HttpVerbTest
{
    public static TheoryData<HttpVerb, HttpVerb> EqualTestData
        =>
        new()
        {
            { HttpVerb.Get, HttpVerb.From("GET") },
            { HttpVerb.Get, HttpVerb.From("get") },
            { HttpVerb.Patch, HttpVerb.From("patch") },
            { HttpVerb.Get, HttpVerb.From("\n\r") }
        };

    public static TheoryData<HttpVerb, HttpVerb> UnequalTestData
        =>
        new()
        {
            { HttpVerb.Get, HttpVerb.Post },
            { HttpVerb.Head, HttpVerb.Delete },
            { HttpVerb.From("custom"), HttpVerb.Get }
        };

    public static TheoryData<HttpVerb?, HttpVerb?> NullableEqualTestData
        =>
        new()
        {
            { null, null }
        };

    public static TheoryData<HttpVerb?, HttpVerb?> NullableUnequalTestData
        =>
        new()
        {
            { null, HttpVerb.Get },
            { HttpVerb.Post, null }
        };
}
