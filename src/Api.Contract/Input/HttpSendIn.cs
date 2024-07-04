using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace GarageGroup.Infra;

public sealed class HttpSendIn(HttpVerb method, [StringSyntax("Uri")] string requestUri) : IEquatable<HttpSendIn>
{
    public HttpVerb Method { get; } = method;

    public string RequestUri { get; } = requestUri.OrEmpty().Trim();

    public FlatArray<KeyValuePair<string, string>> Headers { get; init; }

    public HttpBody Body { get; init; }

    public HttpSuccessType SuccessType { get; init; }

    public override int GetHashCode()
    {
        var builder = new HashCode();
        builder.Add(EqualityComparer<Type>.Default.GetHashCode(typeof(HttpSendIn)));

        builder.Add(EqualityComparer<HttpVerb>.Default.GetHashCode(Method));
        builder.Add(StringComparer.InvariantCulture.GetHashCode(RequestUri));

        builder.Add(EqualityComparer<Type>.Default.GetHashCode(typeof(FlatArray<KeyValuePair<string, string>>)));
        foreach (var header in GetOrderedHeaders(Headers))
        {
            builder.Add(EqualityComparer<Type>.Default.GetHashCode(typeof(KeyValuePair<string, string>)));
            builder.Add(StringComparer.InvariantCultureIgnoreCase.GetHashCode(header.Key));
            builder.Add(StringComparer.InvariantCulture.GetHashCode(header.Value));
        }

        builder.Add(EqualityComparer<HttpBody>.Default.GetHashCode(Body));
        builder.Add(EqualityComparer<HttpSuccessType>.Default.GetHashCode(SuccessType));

        return builder.ToHashCode();
    }

    public override bool Equals(object? obj)
        =>
        Equals(obj as HttpSendIn);

    public bool Equals(HttpSendIn? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (EqualityComparer<HttpVerb>.Default.Equals(Method, other.Method) is false)
        {
            return false;
        }

        if (StringComparer.InvariantCulture.Equals(RequestUri, other.RequestUri) is false)
        {
            return false;
        }

        var orderedHeaders = GetOrderedHeaders(Headers);
        var otherOrderedHeaders = GetOrderedHeaders(other.Headers);

        if (EqualityComparer<FlatArray<KeyValuePair<string, string>>>.Default.Equals(orderedHeaders, otherOrderedHeaders) is false)
        {
            return false;
        }

        if (EqualityComparer<HttpBody>.Default.Equals(Body, other.Body) is false)
        {
            return false;
        }

        return EqualityComparer<HttpSuccessType>.Default.Equals(SuccessType, other.SuccessType);
    }

    public override string ToString()
    {
        var builder = new StringBuilder()
            .AppendFormat("{0} = {1}", nameof(SuccessType), SuccessType).Append('\n')
            .AppendFormat("{0} {1}", Method.Name, RequestUri);

        var orderedHeaders = GetOrderedHeaders(Headers);

        if (orderedHeaders.IsNotEmpty)
        {
            builder = builder.Append('\n');
        }

        foreach (var header in orderedHeaders)
        {
            builder.Append('\n').AppendFormat("{0}: {1}", header.Key, header.Value);
        }

        var body = Body.ToString();
        if (string.IsNullOrEmpty(body))
        {
            return builder.ToString();
        }

        return builder.Append('\n').Append('\n').Append(body).ToString();
    }

    public static bool operator ==(HttpSendIn left, HttpSendIn right)
        =>
        left.Equals(right);

    public static bool operator !=(HttpSendIn left, HttpSendIn right)
        =>
        left.Equals(right) is not true;

    private static FlatArray<KeyValuePair<string, string>> GetOrderedHeaders(FlatArray<KeyValuePair<string, string>> source)
    {
        const char Separator = ',';

        if (source.IsEmpty)
        {
            return [];
        }

        var headers = new Dictionary<string, List<string>>(source.Length, StringComparer.InvariantCultureIgnoreCase);

        foreach (var item in source)
        {
            if (string.IsNullOrWhiteSpace(item.Key))
            {
                continue;
            }

            foreach (var value in item.Value.OrEmpty().Split(Separator))
            {
                var trimmedValue = value.Trim();

                if (headers.TryGetValue(item.Key, out var values))
                {
                    values.Add(trimmedValue);
                    continue;
                }

                headers.Add(item.Key, [trimmedValue]);
            }
        }

        var builder = FlatArray<KeyValuePair<string, string>>.Builder.OfLength(headers.Count);
        var index = 0;

        foreach (var header in headers.OrderBy(GetKey))
        {
            builder[index++] = new(header.Key.ToLowerInvariant(), string.Join(Separator, header.Value));
        }

        return builder.MoveToFlatArray();

        static string GetKey(KeyValuePair<string, List<string>> kv)
            =>
            kv.Key;
    }
}