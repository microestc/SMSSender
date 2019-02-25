using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace SMSSender
{
    public static class SMSSenderExtension
    {
        public static IServiceCollection AddSMSSenderServices(this IServiceCollection services, Action<SMSSenderOptions> setupAction)
        {

            if (setupAction != null)
            {
                services.Configure(setupAction);
            }


            services.AddTransient<HttpClientDelegatingHandler>();

            services.AddHttpClient<ISMSSender, SMSSender>().AddHttpMessageHandler<HttpClientDelegatingHandler>();//.AddPolicyHandler(GetRetryPolicy()).AddPolicyHandler(GetCircuitBreakerPolicy());

            return services;
        }

        internal static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
              .HandleTransientHttpError()
              .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
              .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        }

        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }
}
