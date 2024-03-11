using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace GarageGroup.Infra;

public readonly struct HttpBody : IEquatable<HttpBody>
{
    private const string CharSetUtf8 = "utf-8";

    private static readonly JsonSerializerOptions SerializerOptions;

    static HttpBody()
        =>
        SerializerOptions = new(JsonSerializerDefaults.Web);

    public HttpBodyType Type { get; init; }

    public BinaryData? Content { get; init; }

    public static HttpBody SerializeAsJson<T>(T value)
        =>
        new()
        {
            Type = new(MediaTypeNames.Application.Json, CharSetUtf8),
            Content = BinaryData.FromObjectAsJson(value, SerializerOptions)
        };

    public T? DeserializeFromJson<T>()
    {
        if (Content is null)
        {
            return default;
        }

        return Content.ToObjectFromJson<T>(SerializerOptions);
    }

    public override int GetHashCode()
    {
        var builder = new HashCode();
        builder.Add(EqualityComparer<Type>.Default.GetHashCode(typeof(HttpBody)));

        if (Content is null)
        {
            return builder.ToHashCode();
        }

        builder.Add(EqualityComparer<HttpBodyType>.Default.GetHashCode(Type));

        foreach (var contentByte in Content.ToMemory().Span)
        {
            builder.Add(EqualityComparer<byte>.Default.GetHashCode(contentByte));
        }

        return builder.ToHashCode();
    }

    public override bool Equals(object? obj)
        =>
        obj is HttpBody other && Equals(other);

    public bool Equals(HttpBody other)
    {
        if (Content is null)
        {
            return other.Content is null;
        }

        if (other.Content is null)
        {
            return false;
        }

        if (EqualityComparer<HttpBodyType>.Default.Equals(Type, other.Type) is false)
        {
            return false;
        }

        var data = Content.ToMemory().Span;
        var otherData = other.Content.ToMemory().Span;

        if (data.Length != otherData.Length)
        {
            return false;
        }

        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] == otherData[i])
            {
                continue;
            }

            return false;
        }

        return true;
    }

    public override string? ToString()
    {
        if (Content is null)
        {
            return null;
        }

        var body = Content.ToString();

        if (string.IsNullOrEmpty(Type.MediaType) && string.IsNullOrEmpty(Type.CharSet))
        {
            return body;
        }

        var builder = new StringBuilder("Content-Type: ").Append(Type.MediaType);

        if (string.IsNullOrEmpty(Type.CharSet) is false)
        {
            builder = builder.Append("; charset=").Append(Type.CharSet);
        }

        return builder.AppendLine().Append(body).ToString();
    }

    public static bool operator ==(HttpBody left, HttpBody right)
        =>
        left.Equals(right);

    public static bool operator !=(HttpBody left, HttpBody right)
        =>
        left.Equals(right) is not true;
}