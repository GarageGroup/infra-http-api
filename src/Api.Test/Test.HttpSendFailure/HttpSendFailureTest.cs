using System.Collections.Generic;
using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

public static partial class HttpSendFailureTest
{
    public static TheoryData<HttpSendFailure, HttpSendFailure> EqualTestData
        =>
        new()
        {
            {
                default,
                default
            },
            {
                new()
                {
                    StatusCode = HttpFailureCode.BadGateway,
                    ReasonPhrase = "Bad Gateway",
                    Headers =
                    [
                        new("x-api-version", "2"),
                        new("retry-after", "5")
                    ],
                    Body = new()
                    {
                        Type = new("application/json", "utf-8"),
                        Content = new("{\"error\":\"proxy\"}")
                    }
                },
                new()
                {
                    StatusCode = HttpFailureCode.BadGateway,
                    ReasonPhrase = "Bad Gateway",
                    Headers =
                    [
                        new("x-api-version", "2"),
                        new("retry-after", "5")
                    ],
                    Body = new()
                    {
                        Type = new("application/json", "utf-8"),
                        Content = new("{\"error\":\"proxy\"}")
                    }
                }
            }
        };

    public static TheoryData<HttpSendFailure, HttpSendFailure> UnequalTestData
        =>
        new()
        {
            {
                new()
                {
                    StatusCode = HttpFailureCode.BadRequest
                },
                new()
                {
                    StatusCode = HttpFailureCode.NotFound
                }
            },
            {
                new()
                {
                    StatusCode = HttpFailureCode.BadRequest,
                    ReasonPhrase = "Bad Request"
                },
                new()
                {
                    StatusCode = HttpFailureCode.BadRequest,
                    ReasonPhrase = "Request Failed"
                }
            },
            {
                new()
                {
                    StatusCode = HttpFailureCode.BadGateway,
                    Headers =
                    [
                        new("x-api-version", "2")
                    ]
                },
                new()
                {
                    StatusCode = HttpFailureCode.BadGateway,
                    Headers =
                    [
                        new("x-api-version", "3")
                    ]
                }
            },
            {
                new()
                {
                    StatusCode = HttpFailureCode.BadGateway,
                    Body = new()
                    {
                        Type = new("application/json", "utf-8"),
                        Content = new("{\"error\":\"proxy\"}")
                    }
                },
                new()
                {
                    StatusCode = HttpFailureCode.BadGateway,
                    Body = new()
                    {
                        Type = new("application/json", "utf-8"),
                        Content = new("{\"error\":\"timeout\"}")
                    }
                }
            }
        };
}
