using System;
using System.Collections.Generic;

namespace GarageGroup.Infra;

public sealed record class HttpSendOut
{
    public HttpSuccessCode StatusCode { get; init; }

    public string? ReasonPhrase { get; init; }

    public FlatArray<KeyValuePair<string, string>> Headers { get; init; }

    public HttpBody Body { get; init; }
}