using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing.Patterns;

namespace Microsoft.AspNetCore.Hybrid;

internal sealed class RewriteMetadata
{
    public RoutePattern RoutePattern { get; set; } = RoutePatternFactory.Parse("");
    public string Scheme { get; set; } = "";
    public HostString Host { get; set; } = new HostString("");
}
