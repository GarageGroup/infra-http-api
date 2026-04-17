using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

public static partial class HttpBodyTypeTest
{
    public static TheoryData<HttpBodyType, HttpBodyType> EqualTestData
        =>
        new()
        {
            {
                new(mediaType: null!, charSet: null!),
                new(mediaType: "\n\r", charSet: " ")
            },
            {
                new(mediaType: "application/json", charSet: "utf-8"),
                new(mediaType: "application/json", charSet: "utf-8")
            }
        };

    public static TheoryData<HttpBodyType, HttpBodyType> UnequalTestData
        =>
        new()
        {
            {
                new(mediaType: "application/json", charSet: "utf-8"),
                new(mediaType: "text/plain", charSet: "utf-8")
            },
            {
                new(mediaType: "application/json", charSet: "utf-8"),
                new(mediaType: "application/json", charSet: "utf-16")
            },
            {
                new(mediaType: "application/json", charSet: null!),
                new(mediaType: "application/json", charSet: "utf-8")
            }
        };
}
