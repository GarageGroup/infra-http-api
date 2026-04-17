using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

public static partial class HttpApiTest
{
    public static TheoryData<HttpSuccessType, int, string?, bool, bool> SuccessCaseTestData
        =>
        new()
        {
            { HttpSuccessType.Default, 201, "Created", true, true },
            { HttpSuccessType.OnlyHeaders, 200, "OK", true, false },
            { HttpSuccessType.OnlyStatusCode, 204, "No Content", false, false }
        };

    public static TheoryData<int, string?, string?, string?, string?> FailureCaseTestData
        =>
        new()
        {
            { 404, "Not Found", "{\"error\":\"not found\"}", "application/json", "utf-8" },
            { 503, "Service Unavailable", "temporary outage", "text/plain", "us-ascii" }
        };

    private static HttpApi CreateApi(
        Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync,
        HttpApiOption option = default)
    {
        if (option.BaseAddress is null)
        {
            option = option with
            {
                BaseAddress = new("https://example.com/")
            };
        }

        return new(new DelegateHttpMessageHandler(sendAsync), option);
    }

    private sealed class DelegateHttpMessageHandler(
        Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
            =>
            sendAsync(request, cancellationToken);
    }

    private static HttpResponseMessage CreateResponse(
        int statusCode,
        string? reasonPhrase,
        IEnumerable<KeyValuePair<string, string>> headers,
        string? body,
        string? mediaType = null,
        string? charSet = null)
    {
        var response = new HttpResponseMessage((System.Net.HttpStatusCode)statusCode)
        {
            ReasonPhrase = reasonPhrase
        };

        foreach (var header in headers)
        {
            response.Headers.Add(header.Key, header.Value);
        }

        if (body is null)
        {
            return response;
        }

        response.Content = new StringContent(body);
        if (string.IsNullOrEmpty(mediaType) is false)
        {
            response.Content.Headers.ContentType = new(mediaType)
            {
                CharSet = charSet
            };
        }

        return response;
    }
}
