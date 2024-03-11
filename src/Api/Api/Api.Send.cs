using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

partial class HttpApi
{
    public ValueTask<Result<HttpSendOut, HttpSendFailure>> SendAsync(HttpSendIn input, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (cancellationToken.IsCancellationRequested)
        {
            return ValueTask.FromCanceled<Result<HttpSendOut, HttpSendFailure>>(cancellationToken);
        }

        return InnerSendAsync(input, cancellationToken);
    }

    private async ValueTask<Result<HttpSendOut, HttpSendFailure>> InnerSendAsync(HttpSendIn input, CancellationToken cancellationToken)
    {
        using var httpRequest = BuildHttpRequest(input);
        using var httpResponse = await httpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);

        var isSuccess = httpResponse.IsSuccessStatusCode;
        var response = await ReadResponseAsync(httpResponse, isSuccess ? input.SuccessType : default, cancellationToken).ConfigureAwait(false);

        if (isSuccess)
        {
            return new HttpSendOut
            {
                StatusCode = (HttpSuccessCode)(response.StatusCode - 200),
                ReasonPhrase = response.ReasonPhrase,
                Headers = response.Headers,
                Body = response.Body
            };
        }
        else
        {
            return new HttpSendFailure
            {
                StatusCode = (HttpFailureCode)response.StatusCode,
                ReasonPhrase = response.ReasonPhrase,
                Headers = response.Headers,
                Body = response.Body
            };
        }
    }

    private static HttpRequestMessage BuildHttpRequest(HttpSendIn input)
    {
        var httpRequest = new HttpRequestMessage(
            method: new(input.Method.Name),
            requestUri: input.RequestUri);

        SetHeaders(httpRequest, input.Headers);
        SetBody(httpRequest, input.Body);

        return httpRequest;
    }

    private static async ValueTask<HttpOut> ReadResponseAsync(
        HttpResponseMessage httpResponse, HttpSuccessType type, CancellationToken cancellationToken)
    {
        var headers = type is HttpSuccessType.OnlyStatusCode ? default : GetHeaders(httpResponse).ToFlatArray();
        var body = default(HttpBody);

        if (type is HttpSuccessType.Default)
        {
            body = await ReadBodyAsync(httpResponse.Content, cancellationToken).ConfigureAwait(false);
        }

        return new((int)httpResponse.StatusCode, httpResponse.ReasonPhrase, headers, body);
    }
}