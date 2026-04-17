using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

public static partial class HttpBodyTest
{
    public static TheoryData<HttpBody, HttpBody> EqualTestData
        =>
        new()
        {
            {
                new()
                {
                    Type = new("application/json")
                },
                new()
                {
                    Type = new("text/plain")
                }
            },
            {
                new()
                {
                    Type = new("application/json", "utf-8"),
                    Content = new("{\"value\":1}")
                },
                new()
                {
                    Type = new("application/json", "utf-8"),
                    Content = new("{\"value\":1}")
                }
            }
        };

    public static TheoryData<HttpBody, HttpBody> UnequalTestData
        =>
        new()
        {
            {
                new()
                {
                    Type = new("application/json")
                },
                new()
                {
                    Type = new("application/json"),
                    Content = new("{}")
                }
            },
            {
                new()
                {
                    Type = new("application/json", "utf-8"),
                    Content = new("{\"value\":1}")
                },
                new()
                {
                    Type = new("application/json", "utf-16"),
                    Content = new("{\"value\":1}")
                }
            },
            {
                new()
                {
                    Type = new("application/json", "utf-8"),
                    Content = new("{\"value\":1}")
                },
                new()
                {
                    Type = new("application/json", "utf-8"),
                    Content = new("{\"value\":2}")
                }
            }
        };
}
