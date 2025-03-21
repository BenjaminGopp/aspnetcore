namespace Microsoft.AspNetCore.Hybrid;

public interface IHttpClientMetadata
{
    string HttpClientName { get; }
    Uri? Uri { get; }
}
