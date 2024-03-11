using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using PrimeFuncPack;

namespace GarageGroup.Infra;

public static class HttpApiDependency
{
    public static Dependency<IHttpApi> UseHttpApi(this Dependency<HttpMessageHandler> dependency)
    {
        ArgumentNullException.ThrowIfNull(dependency);

        return dependency.Map<IHttpApi>(ResolveApi);

        static HttpApi ResolveApi(IServiceProvider serviceProvider, HttpMessageHandler httpMessageHandler)
        {
            ArgumentNullException.ThrowIfNull(serviceProvider);
            ArgumentNullException.ThrowIfNull(httpMessageHandler);

            return new(httpMessageHandler, default);
        }
    }

    public static Dependency<IHttpApi> UseHttpApi(this Dependency<HttpMessageHandler> dependency, Func<IServiceProvider, HttpApiOption>? optionResolver)
    {
        ArgumentNullException.ThrowIfNull(dependency);

        return dependency.Map<IHttpApi>(ResolveApi);

        HttpApi ResolveApi(IServiceProvider serviceProvider, HttpMessageHandler httpMessageHandler)
        {
            ArgumentNullException.ThrowIfNull(serviceProvider);
            ArgumentNullException.ThrowIfNull(httpMessageHandler);

            var option = optionResolver is null ? default : optionResolver.Invoke(serviceProvider);
            return new(httpMessageHandler, option);
        }
    }

    public static Dependency<IHttpApi> UseHttpApi(this Dependency<HttpMessageHandler> dependency, string sectionName)
    {
        ArgumentNullException.ThrowIfNull(dependency);

        return dependency.Map<IHttpApi>(ResolveApi);

        HttpApi ResolveApi(IServiceProvider serviceProvider, HttpMessageHandler httpMessageHandler)
        {
            ArgumentNullException.ThrowIfNull(serviceProvider);
            ArgumentNullException.ThrowIfNull(httpMessageHandler);

            var section = serviceProvider.GetServiceOrThrow<IConfiguration>().GetRequiredSection(sectionName.OrEmpty());

            var option = new HttpApiOption
            {
                BaseAddress = section.GetNullableAbsoluteUri("BaseAddress"),
                Timeout = section.GetNullableTimeSpan("Timeout")
            };

            return new(httpMessageHandler, option);
        }
    }

    private static Uri? GetNullableAbsoluteUri(this IConfigurationSection section, string key)
    {
        var value = section[key];
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (Uri.TryCreate(value, UriKind.Absolute, out var uri))
        {
            return uri;
        }

        throw new InvalidOperationException($"Section '{section.Path}' key '{key}' value '{value}' must be an absolute URI value.");
    }

    private static TimeSpan? GetNullableTimeSpan(this IConfigurationSection section, string key)
    {
        var value = section[key];
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (TimeSpan.TryParse(value, out var result))
        {
            return result;
        }

        throw new InvalidOperationException($"Section '{section.Path}' key '{key}' value '{value}' must be a valid TimeSpan value.");
    }
}