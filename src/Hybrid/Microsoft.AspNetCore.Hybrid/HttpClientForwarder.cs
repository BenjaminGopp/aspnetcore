using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hybrid;
using Microsoft.Extensions.Http;

namespace Microsoft.AspNetCore.Hybrid;

internal sealed class HttpClientForwarder : IHttpClientForwarder
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpClientForwarder(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public Task Forward(HttpContext httpContext)
    {
        return Forward(httpContext, httpClientName: "");
    }

    public Task Forward(HttpContext httpContext, Uri? retquestUri)
    {
        return Forward(httpContext, retquestUri ,"");
    }

    public Task Forward(HttpContext httpContext, Uri? retquestUri, string httpClientName)
    {
        if (retquestUri is not null)
        {
            httpContext.Features.Set<IHttpRequestMessageFeature>(new HttpRequestMessageFeature() { RequestUri = retquestUri });
        }
        return Forward(httpContext, httpClientName: httpClientName);

    }

    public async Task Forward(HttpContext httpContext, string httpClientName)
    {
        var httpClient = CreateHttpClient(httpClientName);
        await ForwardCore(httpContext, httpClient);
    }

    private HttpClient CreateHttpClient(string httpClientName)
    {
        HttpClient httpClient;
        if (String.IsNullOrEmpty(httpClientName))
        {
            httpClient = _httpClientFactory.CreateClient();
        }
        else
        {
            httpClient = _httpClientFactory.CreateClient(httpClientName);
        }

        return httpClient;
    }

    private static async Task ForwardCore(HttpContext httpContext, HttpClient httpClient)
    {
        var httpRequestMessage = await httpContext.Request.ToHttpRequestMessageAsync();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        await httpResponseMessage.WriteToHttpContextAsync(httpContext);
    }

}
