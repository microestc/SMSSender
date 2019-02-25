using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SMSSender
{
    internal class HttpClientDelegatingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Conetent-Type", "application/json");
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
