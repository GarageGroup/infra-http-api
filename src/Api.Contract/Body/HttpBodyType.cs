using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;

namespace GarageGroup.Infra;

public readonly record struct HttpBodyType
{
    public HttpBodyType([AllowNull] string mediaType, [AllowNull] string charSet = null)
    {
        MediaType = mediaType.OrNullIfWhiteSpace();
        CharSet = charSet.OrNullIfWhiteSpace();
    }

    public string? MediaType { get; }

    public string? CharSet { get; }

    public bool IsJsonMediaType(bool isApplicationJsonStrict)
    {
        if (string.IsNullOrEmpty(MediaType))
        {
            return false;
        }

        if (string.Equals(MediaType, MediaTypeNames.Application.Json, StringComparison.InvariantCultureIgnoreCase))
        {
            return true;
        }

        if (isApplicationJsonStrict)
        {
            return false;
        }

        return MediaType.Contains("json", StringComparison.InvariantCultureIgnoreCase) is true;
    }
}