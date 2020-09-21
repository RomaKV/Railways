using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HttpClients.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace ServiceRailwayApi
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IRailwayInfo _railwayClient;

        private readonly IStationInfo _stationClient;

        private readonly IUpdateRwyDb _updateRwyDbClient;

        private static DateTime _dateUpdateRailwayInfo;

        private static DateTime _dateUpdateStationInfo;


        public Worker( ILogger<Worker> logger, 
                       IRailwayInfo railwayClient,
                       IStationInfo stationClient,
                       IUpdateRwyDb updateRwyDbClient)
        {
            _logger = logger;
           
            _railwayClient = railwayClient;

            _updateRwyDbClient = updateRwyDbClient;

            _stationClient = stationClient;

        }

        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                
                var resultRailway = await _railwayClient.GetAllAsync();

                if (resultRailway != null)
                {
                    if (resultRailway.DateUpdate > _dateUpdateRailwayInfo)
                    {
                        if (resultRailway.Data?.Any() == true)
                        {                                                                                
                            await _updateRwyDbClient.UpdateRailwayAsync(resultRailway.Data);

                            _dateUpdateRailwayInfo = resultRailway.DateUpdate;
                        }
                    }
                   
                }

                var resultStation = await  _stationClient.GetAllAsync();

                if (resultStation.DateUpdate > _dateUpdateStationInfo)
                {
                        if (resultStation.Data?.Any() == true)
                        {
                            await _updateRwyDbClient.UpdateStationAsync(resultStation.Data);
                        }

                        _dateUpdateStationInfo = resultStation.DateUpdate;
                }
                
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(900000, stoppingToken);
            }
        }
    }
}
