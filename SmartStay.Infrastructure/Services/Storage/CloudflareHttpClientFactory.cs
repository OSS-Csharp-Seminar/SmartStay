using Amazon.Runtime;

namespace SmartStay.Infrastructure.Services.Storage;

public class CloudflareHttpClientFactory : HttpClientFactory
{
    public override HttpClient CreateHttpClient(IClientConfig clientConfig)
    {
        var handler = new HttpClientHandler
        {
            CheckCertificateRevocationList = false
        };
        return new HttpClient(handler);
    }
}
