namespace Microsoft.AspNetCore.Hybrid;

public interface IHttpRequestMessageFeature
{
    Uri? RequestUri { get; set; }
}
