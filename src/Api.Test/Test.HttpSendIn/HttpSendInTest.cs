using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

public static partial class HttpSendInTest
{
    public static TheoryData<HttpSendIn, HttpSendIn> EqualTestData
        =>
        new()
        {
            {
                new(
                    method: HttpVerb.Get,
                    requestUri: "\n\r"),
                new(
                    method: HttpVerb.Get,
                    requestUri: null!)
            },
            {
                new(
                    method: HttpVerb.Post,
                    requestUri: "https://www.example.com/about")
                {
                    SuccessType = HttpSuccessType.OnlyHeaders
                },
                new(
                    method: HttpVerb.Post,
                    requestUri: "https://www.example.com/about")
                {
                    SuccessType = HttpSuccessType.OnlyHeaders
                }
            },
            {
                new(
                    method: HttpVerb.Head,
                    requestUri: "/services/web-development")
                {
                    Headers =
                    [
                        new("x-ms-date", "Wed, 03 Jul 2024 14:41:12 GMT"),
                        new("x-ms-version", "2019-02-02"),
                        new("Accept", "application/json;odata=nometadata")
                    ]
                },
                new(
                    method: HttpVerb.Head,
                    requestUri: "/services/web-development")
                {
                    Headers =
                    [
                        new("x-ms-date", "Wed, 03 Jul 2024 14:41:12 GMT"),
                        new("x-ms-version", "2019-02-02"),
                        new("accept", "application/json;odata=nometadata")
                    ]
                }
            },
            {
                new(
                    method: HttpVerb.Delete,
                    requestUri: "http://www.example.com/blog/post-title")
                {
                    Headers =
                    [
                        new("x-value", " Some text"),
                        new("x-value", "Another text"),
                        new("x-ms-date", "Wed, 03 Jul 2024 14:41:12 GMT"),
                        new("Accept", "application/json;odata=nometadata")
                    ]
                },
                new(
                    method: HttpVerb.Delete,
                    requestUri: "http://www.example.com/blog/post-title")
                {
                    Headers =
                    [
                        new("x-value", "Some text,Another text "),
                        new("x-ms-date", "Wed, 03 Jul 2024 14:41:12 GMT"),
                        new("Accept", "application/json;odata=nometadata"),
                    ]
                }
            },
            {
                new(
                    method: HttpVerb.Post,
                    requestUri: "https://www.example.com/about")
                {
                    Body = new()
                    {
                        Type = new("SomeMediaType")
                    }
                },
                new(
                    method: HttpVerb.Post,
                    requestUri: "https://www.example.com/about")
                {
                    Body = default
                }
            },
            {
                new(
                    method: HttpVerb.Patch,
                    requestUri: "https://www.example.com/contact")
                {
                    Headers =
                    [
                        new("x-value", "Some Value")
                    ],
                    Body = new()
                    {
                        Type = new("SomeMediaType"),
                        Content = new("Some text")
                    }
                },
                new(
                    method: HttpVerb.Patch,
                    requestUri: "https://www.example.com/contact")
                {
                    Headers =
                    [
                        new("X-value", "Some Value")
                    ],
                    Body = new()
                    {
                        Type = new("SomeMediaType"),
                        Content = new("Some text")
                    }
                }
            },
            {
                new(
                    method: HttpVerb.Patch,
                    requestUri: "http://www.example.com/contact")
                {
                    Headers =
                    [
                        new("x-value", "Some Value")
                    ],
                    Body = new()
                    {
                        Type = new("SomeType", "Some CharSet"),
                        Content = new("Some text")
                    }
                },
                new(
                    method: HttpVerb.Patch,
                    requestUri: "http://www.example.com/contact")
                {
                    Headers =
                    [
                        new("X-value", "Some Value")
                    ],
                    Body = new()
                    {
                        Type = new("SomeType", "Some CharSet"),
                        Content = new("Some text")
                    }
                }
            }
        };

    public static TheoryData<HttpSendIn, HttpSendIn> UnequalTestData
        =>
        new()
        {
            {
                new(
                    method: HttpVerb.Get,
                    requestUri: "https://www.example.com/about"),
                new(
                    method: HttpVerb.Get,
                    requestUri: "https://www.example.com/About")
            },
            {
                new(
                    method: HttpVerb.Post,
                    requestUri: "https://www.example.com/about"),
                new(
                    method: HttpVerb.Post,
                    requestUri: "https://www.example.com/about")
                {
                    SuccessType = HttpSuccessType.OnlyHeaders
                }
            },
            {
                new(
                    method: HttpVerb.Put,
                    requestUri: "https://www.example.com/about")
                {
                    SuccessType = HttpSuccessType.OnlyStatusCode
                },
                new(
                    method: HttpVerb.Put,
                    requestUri: "https://www.example.com/about")
                {
                    SuccessType = HttpSuccessType.OnlyHeaders
                }
            },
            {
                new(
                    method: HttpVerb.Delete,
                    requestUri: "http://www.example.com/blog/post-title")
                {
                    Headers =
                    [
                        new("x-value", "Some text"),
                        new("x-ms-date", "Wed, 03 Jul 2024 14:41:12 GMT"),
                        new("Accept", "application/json;odata=nometadata")
                    ]
                },
                new(
                    method: HttpVerb.Delete,
                    requestUri: "http://www.example.com/blog/post-title")
                {
                    Headers =
                    [
                        new("x-value", "some text"),
                        new("x-ms-date", "Wed, 03 Jul 2024 14:41:12 GMT"),
                        new("Accept", "application/json;odata=nometadata"),
                    ]
                }
            },
            {
                new(
                    method: HttpVerb.Post,
                    requestUri: "https://www.example.com/about")
                {
                    Body = new()
                    {
                        Type = new("SomeMediaType"),
                        Content = new("Some text")
                    }
                },
                new(
                    method: HttpVerb.Post,
                    requestUri: "https://www.example.com/about")
                {
                    Body = default
                }
            },
            {
                new(
                    method: HttpVerb.Patch,
                    requestUri: "https://www.example.com/contact")
                {
                    Headers =
                    [
                        new("x-value", "Some Value")
                    ],
                    Body = new()
                    {
                        Type = new("SomeType", "Some CharSet1"),
                        Content = new("Some text")
                    }
                },
                new(
                    method: HttpVerb.Patch,
                    requestUri: "https://www.example.com/contact")
                {
                    Headers =
                    [
                        new("X-value", "Some Value")
                    ],
                    Body = new()
                    {
                        Type = new("SomeType", "Some CharSet"),
                        Content = new("Some text")
                    }
                }
            }
        };
}