namespace Microsoft.AspNetCore.Hybrid;

internal sealed class HttpClientMetadata : IHttpClientMetadata
{

    public HttpClientMetadata(string httpClientName = "", Uri? uri = null)
    {
        HttpClientName = httpClientName;
        Uri = uri;
    }

    public string HttpClientName { get; private set; }
    public Uri? Uri { get; private set; }
}
