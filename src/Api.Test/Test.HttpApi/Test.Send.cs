using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpApiTest
{
    [Theory]
    [MemberData(nameof(SuccessCaseTestData))]
    public static async Task SendAsync_ResponseIsSuccess_ExpectMappedSuccess(
        HttpSuccessType successType,
        int statusCode,
        string? reasonPhrase,
        bool hasHeaders,
        bool hasBody)
    {
        var source = CreateApi(
            sendAsync: (_, _) =>
                Task.FromResult(
                    CreateResponse(
                        statusCode: statusCode,
                        reasonPhrase: reasonPhrase,
                        headers: [new("x-request-id", "abc")],
                        body: "{\"id\":1}",
                        mediaType: "application/json",
                        charSet: "utf-8")));

        var actual = await source.SendAsync(
            new(HttpVerb.Post, "/users")
            {
                SuccessType = successType
            },
            TestContext.Current.CancellationToken);

        Assert.True(actual.IsSuccess);
        var success = actual.SuccessOrThrow();

        Assert.Equal((HttpSuccessCode)(statusCode - 200), success.StatusCode);
        Assert.Equal(reasonPhrase, success.ReasonPhrase);

        if (hasHeaders)
        {
            Assert.Equal(1, success.Headers.Length);
            Assert.True(string.Equals(success.Headers[0].Key, "x-request-id", StringComparison.InvariantCultureIgnoreCase));
            Assert.Equal("abc", success.Headers[0].Value);
        }
        else
        {
            Assert.Equal(default, success.Headers);
        }

        if (hasBody)
        {
            Assert.Equal("application/json", success.Body.Type.MediaType);
            Assert.Equal("utf-8", success.Body.Type.CharSet);
            Assert.Equal("{\"id\":1}", success.Body.Content?.ToString());
        }
        else
        {
            Assert.Equal(default, success.Body);
        }
    }

    [Theory]
    [MemberData(nameof(FailureCaseTestData))]
    public static async Task SendAsync_ResponseIsFailure_ExpectMappedFailure(
        int statusCode,
        string? reasonPhrase,
        string? body,
        string? mediaType,
        string? charSet)
    {
        var source = CreateApi(
            sendAsync: (_, _) =>
                Task.FromResult(
                    CreateResponse(
                        statusCode: statusCode,
                        reasonPhrase: reasonPhrase,
                        headers: [new("x-request-id", "abc")],
                        body: body,
                        mediaType: mediaType,
                        charSet: charSet)));

        var actual = await source.SendAsync(
            new(HttpVerb.Get, "/missing")
            {
                SuccessType = HttpSuccessType.OnlyStatusCode
            },
            TestContext.Current.CancellationToken);

        Assert.True(actual.IsFailure);
        var failure = actual.FailureOrThrow();

        Assert.Equal((HttpFailureCode)statusCode, failure.StatusCode);
        Assert.Equal(reasonPhrase, failure.ReasonPhrase);
        Assert.Equal(1, failure.Headers.Length);
        Assert.True(string.Equals(failure.Headers[0].Key, "x-request-id", StringComparison.InvariantCultureIgnoreCase));
        Assert.Equal("abc", failure.Headers[0].Value);

        if (body is null)
        {
            Assert.Equal(default, failure.Body);
            return;
        }

        Assert.Equal(mediaType, failure.Body.Type.MediaType);
        Assert.Equal(charSet, failure.Body.Type.CharSet);
        Assert.Equal(body, failure.Body.Content?.ToString());
    }

    [Fact]
    public static async Task SendAsync_InputIsNull_ExpectArgumentNullException()
    {
        var source = CreateApi(
            sendAsync: (_, _) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));

        await Assert.ThrowsAsync<ArgumentNullException>(
            () => source.SendAsync(null!, TestContext.Current.CancellationToken).AsTask());
    }

    [Fact]
    public static async Task SendAsync_InputHasHeadersAndBody_ExpectRequestMapped()
    {
        HttpMethod? actualMethod = null;
        string? actualHeaderValue = null;
        string? actualContentType = null;
        string? actualBody = null;

        var source = CreateApi(
            sendAsync: async (request, cancellationToken) =>
            {
                actualMethod = request.Method;
                actualHeaderValue = request.Headers.GetValues("x-request-id").SingleOrDefault();
                actualContentType = request.Content?.Headers.ContentType?.ToString();
                actualBody = request.Content is null
                    ? null
                    : await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

                return new HttpResponseMessage(HttpStatusCode.OK);
            });

        _ = await source.SendAsync(
            new(HttpVerb.Patch, "/users/1")
            {
                Headers = [new("x-request-id", "abc")],
                Body = new()
                {
                    Type = new("application/json", "utf-8"),
                    Content = new("{\"firstName\":\"John\"}")
                }
            },
            TestContext.Current.CancellationToken);

        Assert.Equal(HttpMethod.Patch, actualMethod);
        Assert.Equal("abc", actualHeaderValue);
        Assert.Equal("application/json; charset=utf-8", actualContentType);
        Assert.Equal("{\"firstName\":\"John\"}", actualBody);
    }

    [Fact]
    public static async Task SendAsync_InputHasDuplicateHeaders_ExpectAllValuesMapped()
    {
        string[] actualHeaderValues = [];
        var source = CreateApi(
            sendAsync: (request, _) =>
            {
                actualHeaderValues = request.Headers.GetValues("x-request-id").ToArray();
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            });

        _ = await source.SendAsync(
            new(HttpVerb.Get, "/users")
            {
                Headers =
                [
                    new("x-request-id", "abc"),
                    new("X-Request-ID", "def")
                ]
            },
            TestContext.Current.CancellationToken);

        Assert.Equal(["abc", "def"], actualHeaderValues);
    }

    [Fact]
    public static async Task SendAsync_InputBodyIsEmpty_ExpectRequestWithoutContent()
    {
        var hasContent = true;
        var source = CreateApi(
            sendAsync: (request, _) =>
            {
                hasContent = request.Content is not null;
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            });

        _ = await source.SendAsync(
            new(HttpVerb.Get, "/users")
            {
                Body = default
            },
            TestContext.Current.CancellationToken);

        Assert.False(hasContent);
    }
}
