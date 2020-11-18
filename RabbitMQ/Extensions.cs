using System;
using Actio.Common.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.vNext;
using RawRabbit.Configuration;
using RawRabbit.Extensions.Client;
using IBusClient = RawRabbit.IBusClient;


namespace RabbitMQ
{
    public static class Extensions
    {
        public static IBusClient AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {

            var config = new RabbitMqOptions();
            var section = configuration.GetSection("RabbitMQ");
            section.Bind(config);
            var client = BusClientFactory.CreateDefault(config);
            
            services.AddSingleton<IBusClient>(_ => client);

            return client;
        }
    }
}
