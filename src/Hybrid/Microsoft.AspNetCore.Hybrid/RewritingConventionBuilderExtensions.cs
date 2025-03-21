using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing.Patterns;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.AspNetCore.Hybrid;

public static class RewritingConventionBuilderExtensions
{

    //public static IApplicationBuilder UseRewriting(this IApplicationBuilder builder)
    //{
    //    return builder.UseMiddleware<RewritingMiddleware>();
    //}

    public static IEndpointConventionBuilder Rewrite(this IEndpointConventionBuilder builder, [StringSyntax("Route")] string pattern)
    {
        builder.Add(endpointBuilder =>
        {
            endpointBuilder.Metadata.Add(new RewriteMetadata() { RoutePattern = RoutePatternFactory.Parse(pattern)});
        });

        return builder;
    }

    public static IEndpointConventionBuilder Rewrite(this IEndpointConventionBuilder builder, RoutePattern pattern)
    {
        builder.Add(endpointBuilder =>
        {
            endpointBuilder.Metadata.Add(new RewriteMetadata() { RoutePattern = pattern });
        });

        return builder;
    }

    public static IEndpointConventionBuilder Rewrite(this IEndpointConventionBuilder builder, string scheme, string host, [StringSyntax("Route")] string pattern)
    {
        builder.Add(endpointBuilder =>
        {
            endpointBuilder.Metadata.Add(new RewriteMetadata() { RoutePattern = RoutePatternFactory.Parse(pattern), Scheme = scheme, Host = new HostString(host) });
        });

        return builder;
    }

    public static IEndpointConventionBuilder Rewrite(this IEndpointConventionBuilder builder, string scheme, HostString host, RoutePattern pattern)
    {
        builder.Add(endpointBuilder =>
        {
            endpointBuilder.Metadata.Add(new RewriteMetadata() { RoutePattern = pattern, Scheme = scheme, Host = host });
        });

        return builder;
    }

}
