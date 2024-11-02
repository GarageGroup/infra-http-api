using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

internal sealed partial class HttpApi : IHttpApi
{
    private readonly HttpClient httpClient;

    internal HttpApi(HttpMessageHandler httpMessageHandler, HttpApiOption option)
    {
        httpClient = new(httpMessageHandler, disposeHandler: false)
        {
            BaseAddress = option.BaseAddress
        };

        if (option.Timeout is not null)
        {
            httpClient.Timeout = option.Timeout.Value;
        }
    }

    private static void SetHeaders(HttpRequestMessage httpRequest, FlatArray<KeyValuePair<string, string>> headers)
    {
        if (headers.IsEmpty)
        {
            return;
        }

        var headerValues = new Dictionary<string, List<string>>(headers.Length, StringComparer.InvariantCultureIgnoreCase);

        foreach (var header in headers)
        {
            if (string.IsNullOrWhiteSpace(header.Key))
            {
                continue;
            }

            if (headerValues.TryGetValue(header.Key, out var values))
            {
                values.Add(header.Value.OrEmpty());
            }

            headerValues.Add(header.Key, [header.Value]);
        }

        foreach (var header in headerValues)
        {
            if (httpRequest.Headers.Contains(header.Key))
            {
                httpRequest.Headers.Remove(header.Key);
            }

            httpRequest.Headers.Add(header.Key, header.Value);
        }
    }

    private static void SetBody(HttpRequestMessage httpRequest, HttpBody body)
    {
        if (body.Content is null)
        {
            return;
        }

        httpRequest.Content = new ByteArrayContent(body.Content.ToArray());

        if (string.IsNullOrEmpty(body.Type.MediaType) is false)
        {
            httpRequest.Content.Headers.ContentType = new(body.Type.MediaType, body.Type.CharSet);
        }
    }

    private static IEnumerable<KeyValuePair<string, string>> GetHeaders(HttpResponseMessage httpResponse)
    {
        foreach (var header in httpResponse.Headers)
        {
            foreach (var value in header.Value)
            {
                yield return new(header.Key, value);
            }
        }
    }

    private static async Task<HttpBody> ReadBodyAsync(HttpContent httpContent, CancellationToken cancellationToken)
    {
        var bytes = await httpContent.ReadAsByteArrayAsync(cancellationToken).ConfigureAwait(false);
        if (bytes.Length is 0)
        {
            return default;
        }

        return new()
        {
            Type = new(httpContent.Headers.ContentType?.MediaType, httpContent.Headers.ContentType?.CharSet),
            Content = BinaryData.FromBytes(bytes)
        };
    }

    private sealed record class HttpOut(int StatusCode, string? ReasonPhrase, FlatArray<KeyValuePair<string, string>> Headers, HttpBody Body);
}