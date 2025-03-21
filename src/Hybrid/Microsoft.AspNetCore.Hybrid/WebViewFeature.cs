namespace Microsoft.AspNetCore.Hybrid;

public sealed class WebViewFeature : IWebViewFeature
{
    public string? WebViewName { get; set; }
    public string? Origin { get; set; }
}
