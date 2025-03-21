using Microsoft.AspNetCore.Hybrid;

namespace Microsoft.AspNetCore.Http;

internal sealed class HttpClientMiddleware
{

    private readonly RequestDelegate _next;
    private readonly IHttpClientForwarder _forwarder;

    public HttpClientMiddleware(RequestDelegate next, IHttpClientForwarder forwarder)
    {
        _next = next;
        _forwarder = forwarder;
    }


    public async Task InvokeAsync(HttpContext httpContext)
    {
        
        var endpoint = httpContext.GetEndpoint()?.Metadata.GetMetadata<IHttpClientMetadata>();
        if (endpoint is not null)
        {            
            await _forwarder.Forward(httpContext, endpoint.Uri, endpoint.HttpClientName);
        }        
        await _next(httpContext);        

    }

}



