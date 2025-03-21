namespace Microsoft.AspNetCore.Hybrid;

public interface IWebViewFeature
{
    string? WebViewName { get; set; }
    string? Origin { get; set; }
}
