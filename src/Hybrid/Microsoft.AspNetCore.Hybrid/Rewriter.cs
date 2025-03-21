//using Microsoft.AspNetCore.Hybrid;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.Extensions;
//using Microsoft.AspNetCore.Routing;
//using Microsoft.AspNetCore.Routing.Patterns;
//using Microsoft.AspNetCore.Routing.Template;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection.Extensions;
//using Microsoft.Extensions.Diagnostics.HealthChecks;
//using Microsoft.Extensions.Hosting;
//using System.Diagnostics.CodeAnalysis;
//using System.Reflection;

//namespace Microsoft.AspNetCore.Hybrid;


//internal sealed class RewritingMiddleware
//{

//    private readonly RequestDelegate _next;
//    private readonly LinkGenerator _linkGenerator;

//    public RewritingMiddleware(RequestDelegate next, LinkGenerator linkGenerator)
//    {
//        _next = next;
//        _linkGenerator = linkGenerator;
//    }


//    public async Task InvokeAsync(HttpContext httpContext)
//    {

//        var rewriteMetadata = httpContext.GetEndpoint()?.Metadata.GetMetadata<RewriteMetadata>();
//        if (rewriteMetadata is null)
//        {
//            await _next(httpContext);
//            return;
//        }

//        var endpoint = httpContext.GetEndpoint() as RouteEndpoint;
//        var routePattern = endpoint.RoutePattern;

//        var rd = httpContext.GetRouteData();
//        var values = rd.Values;


//        var endpointBuilder = new RouteEndpointBuilder([EndpointName("temp")] async (c) => { }, rewriteMetadata.RoutePattern, 0);


//        string link = GetPathByEndpoints(_linkGenerator, httpContext, [endpointBuilder.Build() as RouteEndpoint], values, null, "", new FragmentString(), null);
//        if (rewriteMetadata.Host.HasValue)
//        {
//            link = GetUriByEndpoints(_linkGenerator, [endpointBuilder.Build() as RouteEndpoint], values, null, rewriteMetadata.Scheme, rewriteMetadata.Host, "", new FragmentString(), null);
//        }

//        httpContext.Features.Get<IHttpRequestMessageFeature>().RequestUri ??= new Uri(link);

//        await _next(httpContext);
//    }

//    public static string? GetUriByEndpoints(LinkGenerator obj,
//    List<RouteEndpoint> endpoints,
//    RouteValueDictionary values,
//    RouteValueDictionary? ambientValues,
//    string scheme,
//    HostString host,
//    PathString pathBase,
//    FragmentString fragment,
//    LinkOptions? options)
//    {
//        return (string)obj.GetType().GetMethod("GetUriByEndpoints").Invoke(obj,
//            new object[] { endpoints, values, ambientValues, scheme, host, pathBase, fragment, options });
//    }

//    public static string? GetPathByEndpoints(LinkGenerator obj,
//       HttpContext? httpContext,
//       List<RouteEndpoint> endpoints,
//       RouteValueDictionary values,
//       RouteValueDictionary? ambientValues,
//       PathString pathBase,
//       FragmentString fragment,
//       LinkOptions? options)
//    {
//        return (string)obj.GetType().GetMethod("GetPathByEndpoints",BindingFlags.Instance | BindingFlags.NonPublic).Invoke(obj,
//    new object[] { httpContext, endpoints, values, ambientValues, pathBase, fragment, options });
//    }

//}
