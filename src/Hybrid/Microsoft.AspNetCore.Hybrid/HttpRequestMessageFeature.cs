namespace Microsoft.AspNetCore.Hybrid;

public sealed class HttpRequestMessageFeature : IHttpRequestMessageFeature
{
    public Uri? RequestUri { get; set; }
}
