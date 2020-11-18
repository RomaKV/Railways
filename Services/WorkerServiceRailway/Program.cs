using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HttpClients;
using HttpClients.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ;

namespace ServiceRailwayApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {

                    IConfiguration configuration = hostContext.Configuration;

                    services.AddSingleton<IRailwayInfo>(x => new RailwayClient(configuration.GetSection("ConnectionUgmk").Value));

                    services.AddSingleton<IStationInfo>(x => new StationClient(configuration.GetSection("ConnectionUgmk").Value));

                    services.AddSingleton<IUpdateRwyDb>(x =>
                        new UpdateRwyDbClient(configuration.GetSection("ConnectionServiceDb").Value));

                    services.AddRabbitMq(configuration);

                    services.AddHostedService<Worker>();




                });
    }
}
