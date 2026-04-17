using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpApiTest
{
    [Fact]
    public static async Task Constructor_BaseAddressProvided_ExpectRequestUriCombined()
    {
        Uri? actualUri = null;

        var source = CreateApi(
            sendAsync: (request, _) =>
            {
                actualUri = request.RequestUri;
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            },
            option: new()
            {
                BaseAddress = new("https://example.com/root/")
            });

        _ = await source.SendAsync(
            new(HttpVerb.Get, "products"),
            TestContext.Current.CancellationToken);

        Assert.Equal(new("https://example.com/root/products"), actualUri);
    }
}
