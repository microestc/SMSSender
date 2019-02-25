using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SMSSender
{
    internal class HttpClientDelegatingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            
            // do some things
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
