using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace SMSSender
{
    public class SMSSender : ISMSSender
    {
        private readonly SMSSenderOptions _options;
        private readonly HttpClient _httpClient;

        public SMSSender(IOptions<SMSSenderOptions> options, HttpClient httpClient)
        {
            _options = options.Value;
            _httpClient = httpClient;
        }

        public async Task SendAsync(string destination, string message)
        {
            if (_options.UsePostMethod)
            {
                var httpContent = _options.HttpContent(destination, message);
                var response = await _httpClient.PostAsync(_options.AppUrl, httpContent);
                if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception("post method send sms failed, try later.");
                }
                response.EnsureSuccessStatusCode();
            }
            var requestUri = _options.RequestUri(destination, message);
            await _httpClient.GetStringAsync(requestUri: requestUri);
        }

    }
}
