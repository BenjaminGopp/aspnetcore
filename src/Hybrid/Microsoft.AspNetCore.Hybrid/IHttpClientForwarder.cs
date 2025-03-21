using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Hybrid;

public interface IHttpClientForwarder
{
    Task Forward(HttpContext httpContext);
    Task Forward(HttpContext httpContext, Uri? uri);
    Task Forward(HttpContext httpContext, Uri? uri, string httpClientName);
    Task Forward(HttpContext httpContext, string httpClientName);
    
}
