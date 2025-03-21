using Microsoft.AspNetCore.Hybrid;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder;

public static class ServiceWorkerExtensions
{

    public static IServiceCollection AddServiceWorker(this IServiceCollection services, Action<OutputCaching.OutputCacheOptions>? configureOutputCache = null, Action<Cors.Infrastructure.CorsOptions>? configureCors = null)
    {
        
        services.AddTransient<IHttpClientForwarder, HttpClientForwarder>();
        services.AddHttpClient();
        if (configureCors is not null)
        {
            services.AddCors(configureCors);
        }
        else
        {
            services.AddCors();
        }

        if (configureOutputCache is not null)
        {
            services.AddOutputCache(configureOutputCache);
        }
        else
        {
            services.AddOutputCache();
        }
        
        return services;
    }

    public static IApplicationBuilder UseServiceWorker(this IApplicationBuilder builder, Action<IEndpointRouteBuilder> configure)
    {

        builder.MapWhen(context => context.Request.IsSameOrigin() == false, config =>
        {                      
            config.UseRouting();
            config.UseCors();
            config.UseOutputCache();
            //config.UseRewriting();
            config.UseHttpForwarding();
            config.UseEndpoints(endpoints =>
            {
                configure(endpoints);
            });

        });

        return builder;
    }


    private static bool IsSameOrigin(this HttpRequest request)
    {
        var origin = request.HttpContext.Features.Get<IWebViewFeature>()?.Origin ?? "";
        return request.Host.Host == origin;
    }
    

}
