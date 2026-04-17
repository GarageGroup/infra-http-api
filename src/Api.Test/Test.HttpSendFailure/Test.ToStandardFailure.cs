using System;
using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpSendFailureTest
{
    [Fact]
    public static void ToStandardFailure_BaseMessageIsDefaultNoReasonNoBody_ExpectMessageWithStatusCode()
    {
        var source = new HttpSendFailure
        {
            StatusCode = HttpFailureCode.BadRequest
        };

        var actual = source.ToStandardFailure();
        var expected = Failure.Create(HttpFailureCode.BadRequest, "An unexpected http failure occured: 400.");

        Assert.Equal(expected, actual);
    }

    [Fact]
    public static void ToStandardFailure_BaseMessageIsWhiteSpaceAndReasonPresentNoBody_ExpectMessageWithoutBaseMessage()
    {
        var source = new HttpSendFailure
        {
            StatusCode = HttpFailureCode.NotFound,
            ReasonPhrase = "Not Found"
        };

        var actual = source.ToStandardFailure(" \n\r ");
        var expected = Failure.Create(HttpFailureCode.NotFound, "404 Not Found.");

        Assert.Equal(expected, actual);
    }

    [Fact]
    public static void ToStandardFailure_BaseMessageAndReasonAndBodyPresent_ExpectMessageWithBody()
    {
        var source = new HttpSendFailure
        {
            StatusCode = HttpFailureCode.BadGateway,
            ReasonPhrase = "Bad Gateway",
            Body = new()
            {
                Type = new("application/json", "utf-8"),
                Content = new("{\"error\":\"proxy\"}")
            }
        };

        var actual = source.ToStandardFailure("Failed to execute request:");
        var expected = Failure.Create(HttpFailureCode.BadGateway, "Failed to execute request: 502 Bad Gateway.\n{\"error\":\"proxy\"}");

        Assert.Equal(expected, actual);
    }

    [Fact]
    public static void ToStandardFailure_ReasonIsWhiteSpaceBodyPresent_ExpectMessageWithoutReason()
    {
        var source = new HttpSendFailure
        {
            StatusCode = HttpFailureCode.ServiceUnavailable,
            ReasonPhrase = " ",
            Body = new()
            {
                Content = new("Some error body")
            }
        };

        var actual = source.ToStandardFailure("Request failed:");
        var expected = Failure.Create(HttpFailureCode.ServiceUnavailable, "Request failed: 503.\nSome error body");

        Assert.Equal(expected, actual);
    }
}
