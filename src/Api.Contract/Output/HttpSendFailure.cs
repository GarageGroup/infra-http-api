using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GarageGroup.Infra;

public readonly record struct HttpSendFailure
{
    private const string DefaultFailureMessageBase = "An unexpected http failure occured:";

    public HttpFailureCode StatusCode { get; init; }

    public string? ReasonPhrase { get; init; }

    public FlatArray<KeyValuePair<string, string>> Headers { get; init; }

    public HttpBody Body { get; init; }

    public Failure<HttpFailureCode> ToStandardFailure([AllowNull] string baseMessage = DefaultFailureMessageBase)
    {
        var builder = new StringBuilder();

        if (string.IsNullOrWhiteSpace(baseMessage) is false)
        {
            builder = builder.Append(baseMessage).Append(' ');
        }

        builder = builder.Append((int)StatusCode);
        if (string.IsNullOrWhiteSpace(ReasonPhrase) is false)
        {
            builder = builder.Append(' ').Append(ReasonPhrase);
        }

        builder = builder.Append('.');
        if (Body.Content is null)
        {
            return new(StatusCode, builder.ToString());
        }

        builder = builder.Append('\n').Append(Body.Content.ToString());
        return new(StatusCode, builder.ToString());
    }
}