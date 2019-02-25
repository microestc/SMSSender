using System;
using Microsoft.Extensions.DependencyInjection;
using Polly;

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

            services.AddHttpClient<ISMSSender, SMSSender>(option =>
            {
                option.DefaultRequestHeaders.Add("Conetent-Type", "application/json");
            })
            .AddHttpMessageHandler<HttpClientDelegatingHandler>()
            .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)))
            .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(10)));

            return services;
        }

    }
}
