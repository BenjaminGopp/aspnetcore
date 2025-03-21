using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Hybrid;

public static class HttpClientEndpointConventionBuilderExtensions
{

    public static IApplicationBuilder UseHttpForwarding(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HttpClientMiddleware>();
    }

    public static IEndpointConventionBuilder Forward(this IEndpointConventionBuilder builder)
    {
        builder.Add(endpointBuilder =>
        {
            endpointBuilder.Metadata.Add(new HttpClientMetadata(""));
        });

        return builder;
    }

    public static IEndpointConventionBuilder Forward(this IEndpointConventionBuilder builder, Uri uri)
    {
        builder.Add(endpointBuilder =>
        {
            endpointBuilder.Metadata.Add(new HttpClientMetadata("", uri));
        });

        return builder;
    }

    public static IEndpointConventionBuilder Forward(this IEndpointConventionBuilder builder, Uri uri, string httpClientName)
    {
        builder.Add(endpointBuilder =>
        {
            endpointBuilder.Metadata.Add(new HttpClientMetadata(httpClientName, uri));
        });

        return builder;
    }

    public static IEndpointConventionBuilder ForwardTo(this IEndpointConventionBuilder builder, string httpClientName)
    {
        builder.Add(endpointBuilder =>
        {
            endpointBuilder.Metadata.Add(new HttpClientMetadata(httpClientName));
        });

        return builder;
    }

}
