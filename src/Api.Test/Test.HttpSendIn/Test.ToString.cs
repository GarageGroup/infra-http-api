using Xunit;

namespace GarageGroup.Infra.Http.Api.Test;

partial class HttpSendInTest
{
    [Theory]
    [MemberData(nameof(ToStringTestData))]
    public static void ToString_ExpectCorrectString(
        HttpSendIn source, string expected)
    {
        var actual = source.ToString();
        Assert.Equal(expected, actual);
    }

    public static TheoryData<HttpSendIn, string> ToStringTestData
        =>
        new()
        {
            {
                new(
                    method: HttpVerb.Get,
                    requestUri: "\n\r"),
                "SuccessType = Default\nGET "
            },
            {
                new(
                    method: HttpVerb.Get,
                    requestUri: null!),
                "SuccessType = Default\nGET "
            },
            {
                new(
                    method: HttpVerb.Post,
                    requestUri: "https://www.example.com/about")
                {
                    SuccessType = HttpSuccessType.OnlyHeaders
                },
                "SuccessType = OnlyHeaders\nPOST https://www.example.com/about"
            },
            {
                new(
                    method: HttpVerb.Head,
                    requestUri: "/services/web-development")
                {
                    Headers =
                    [
                        new("x-ms-version", "2019-02-02"),
                        new("Accept", "application/json;odata=nometadata")
                    ]
                },
                "SuccessType = Default\nHEAD /services/web-development" +
                "\n\naccept: application/json;odata=nometadata\nx-ms-version: 2019-02-02"
            },
            {
                new(
                    method: HttpVerb.Post,
                    requestUri: "https://www.example.com/about")
                {
                    Body = new()
                    {
                        Type = new("SomeMediaType")
                    }
                },
                "SuccessType = Default\nPOST https://www.example.com/about"
            },
            {
                new(
                    method: HttpVerb.Patch,
                    requestUri: "http://www.example.com/contact")
                {
                    Headers =
                    [
                        new("x-value", "Some Value")
                    ],
                    Body = new()
                    {
                        Type = new("SomeType", "Some CharSet"),
                        Content = new("Some text")
                    }
                },
                "SuccessType = Default\nPATCH http://www.example.com/contact" +
                "\n\nx-value: Some Value\n\nContent-Type: SomeType; charset=Some CharSet\nSome text"
            }
        };
}