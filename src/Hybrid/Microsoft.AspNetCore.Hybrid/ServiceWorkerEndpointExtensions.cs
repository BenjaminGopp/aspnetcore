using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.AspNetCore.Routing;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Builder;

public static class ServiceWorkerEndpointRouteBuilderExtensions
{

    static readonly RequestDelegate EmptyRequestDelegate = _ => Task.CompletedTask;

    public static RouteGroupBuilder MapGroup(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGroup("");
    }

    public static RouteGroupBuilder MapHost(this IEndpointRouteBuilder endpoints, params string[] hosts)
    {
        return endpoints.MapGroup().RequireHost(hosts);
    }

    public static IEndpointConventionBuilder Map(this IEndpointRouteBuilder endpoints,[StringSyntax("Route")] string pattern)        
    {
        return ServiceWorkerEndpointRouteBuilderExtensions.Map(endpoints, RoutePatternFactory.Parse(pattern));
    }

    public static IEndpointConventionBuilder MapAll(this IEndpointRouteBuilder endpoints)
    {
        return ServiceWorkerEndpointRouteBuilderExtensions.Map(endpoints, RoutePatternFactory.Parse("{*all}"));
    }


    public static IEndpointConventionBuilder Map(this IEndpointRouteBuilder endpoints, RoutePattern pattern)
    {
        return EndpointRouteBuilderExtensions.Map(endpoints, pattern, EmptyRequestDelegate);
    }

    public static IEndpointConventionBuilder MapGet(this IEndpointRouteBuilder endpoints, [StringSyntax("Route")] string pattern)
    {
        return EndpointRouteBuilderExtensions.MapGet(endpoints, pattern, EmptyRequestDelegate);
    }

    public static IEndpointConventionBuilder MapPost(this IEndpointRouteBuilder endpoints, [StringSyntax("Route")] string pattern)
    {
        return EndpointRouteBuilderExtensions.MapGet(endpoints, pattern, EmptyRequestDelegate);
    }


}

