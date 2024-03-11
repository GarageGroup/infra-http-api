using System;

namespace GarageGroup.Infra;

public readonly record struct HttpApiOption
{
    public Uri? BaseAddress { get; init; }

    public TimeSpan? Timeout { get; init; }
}