using System;
using System.Collections.Generic;

namespace GarageGroup.Infra;

public sealed class HttpVerb : IEquatable<HttpVerb>
{
    public static HttpVerb Get { get; }

    public static HttpVerb Head { get; }

    public static HttpVerb Post { get; }

    public static HttpVerb Put { get; }

    public static HttpVerb Delete { get; }

    public static HttpVerb Connect { get; }

    public static HttpVerb Options { get; }

    public static HttpVerb Trace { get; }

    public static HttpVerb Patch { get; }

    static HttpVerb()
    {
        Get = new("GET");
        Head = new("HEAD");
        Post = new("POST");
        Put = new("PUT");
        Delete = new("DELETE");
        Connect = new("CONNECT");
        Options = new("OPTIONS");
        Trace = new("TRACE");
        Patch = new("PATCH");
    }

    public static HttpVerb From(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Get;
        }

        return new(name.ToUpperInvariant());
    }

    private HttpVerb(string name)
        =>
        Name = name;

    public string Name { get; }

    public override string ToString()
        =>
        Name;

    public override int GetHashCode()
    {
        var builder = new HashCode();

        builder.Add(EqualityComparer<Type>.Default.GetHashCode(typeof(HttpVerb)));
        builder.Add(StringComparer.InvariantCultureIgnoreCase.GetHashCode(Name));

        return builder.ToHashCode();
    }

    public override bool Equals(object? obj)
        =>
        Equals(obj as HttpVerb);

    public bool Equals(HttpVerb? other)
    {
        if (other is null)
        {
            return false;
        }

        return StringComparer.InvariantCultureIgnoreCase.Equals(Name, other.Name);
    }

    public static bool operator ==(HttpVerb left, HttpVerb right)
        =>
        left.Equals(right);

    public static bool operator !=(HttpVerb left, HttpVerb right)
        =>
        left.Equals(right) is not true;
}