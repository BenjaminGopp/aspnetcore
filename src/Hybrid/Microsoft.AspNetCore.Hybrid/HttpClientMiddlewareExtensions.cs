using Microsoft.AspNetCore.Hybrid;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Primitives;
using System.Net.Http;
using System.Linq;

namespace Microsoft.AspNetCore.Hybrid;

public static class HttpClientMiddlewareExtensions
{

    public static Task<HttpRequestMessage> ToHttpRequestMessageAsync(this HttpRequest httpRequest)
    {
        var httpRequestMessageFeature = httpRequest.HttpContext.Features.Get<IHttpRequestMessageFeature>();

        var httpRequestMessage = new HttpRequestMessage();
        httpRequestMessage.RequestUri = httpRequestMessageFeature?.RequestUri;
        httpRequestMessage.Method = HttpMethod.Parse(httpRequest.Method);
        httpRequestMessage.Content = new StreamContent(httpRequest.Body); //copy??
        foreach (var h in httpRequest.Headers)
        {
            if (!httpRequestMessage.Headers.TryAddWithoutValidation(h.Key, [h.Value]))
            {
                httpRequestMessage.Content.Headers.TryAddWithoutValidation(h.Key, [h.Value]);
            }
        }

        return Task.FromResult(httpRequestMessage);
    }

    public static async Task WriteToHttpContextAsync(this HttpResponseMessage httpResponseMessage, HttpContext context)
    {
        context.Response.StatusCode = (int)httpResponseMessage.StatusCode;
        foreach (var h in httpResponseMessage.Headers)
        {
            context.Response.Headers.Add(h.Key, new StringValues(h.Value.ToArray()));
        }
        foreach (var h in httpResponseMessage.Content.Headers)
        {
            context.Response.Headers.Add(h.Key, new StringValues(h.Value.ToArray()));
        }

        if (httpResponseMessage.Content is not null)
        {
            var stream = httpResponseMessage.Content.ReadAsStream();
            await stream.CopyToAsync(context.Response.Body); //does not block at all
        }        
    }

}
