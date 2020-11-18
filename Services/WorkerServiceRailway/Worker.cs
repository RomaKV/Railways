using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HttpClients.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Events;
using RawRabbit;

namespace ServiceRailwayApi
{
    public class Worker : BackgroundService
    {
        private static DateTime _dateUpdateRailwayInfo = DateTime.MinValue;

        private static DateTime _dateUpdateStationInfo = DateTime.MinValue;

        private readonly IBusClient _busClient;
        private readonly ILogger<Worker> _logger;

        private readonly IRailwayInfo _railwayClient;

        private readonly IStationInfo _stationClient;

        private readonly IUpdateRwyDb _updateRwyDbClient;


        public Worker(ILogger<Worker> logger,
            IRailwayInfo railwayClient,
            IStationInfo stationClient,
            IUpdateRwyDb updateRwyDbClient,
            IBusClient busClient)
        {
            _logger = logger;

            _railwayClient = railwayClient;

            _updateRwyDbClient = updateRwyDbClient;

            _stationClient = stationClient;

            _busClient = busClient;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var resultRailway = await _railwayClient.GetAllAsync();


                if (resultRailway != null)
                {
                    _logger.LogInformation($"Got resultRailway count: {resultRailway?.Data.Count()}");
                    if (resultRailway.DateUpdate > _dateUpdateRailwayInfo)
                    {
                        if (resultRailway.Data?.Any() == true)
                        {
                            _logger.LogInformation($"Saving railway [{DateTime.Now}]");
                            await _updateRwyDbClient.UpdateRailwayAsync(resultRailway.Data);
                            _logger.LogInformation($"Saved railway [{DateTime.Now}]");

                            _dateUpdateRailwayInfo = resultRailway.DateUpdate;
                        }
                    }
                }


                var resultStation = await _stationClient.GetAllAsync();

                if (resultStation != null)
                {
                    _logger.LogInformation($"Got resultStation count: {resultStation?.Data.Count()}");

                    if (resultStation.DateUpdate > _dateUpdateStationInfo)
                    {
                        if (resultStation.Data?.Any() == true)
                        {
                            await _updateRwyDbClient.UpdateStationAsync(resultStation.Data);
                        }

                        await _busClient.PublishAsync(new UpdateInfoStationsEvent(DateTime.Now));

                        _dateUpdateStationInfo = resultStation.DateUpdate;
                    }
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(900000, stoppingToken);
            }
        }
    }
}