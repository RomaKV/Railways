using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using HttpClients.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Events;
using RawRabbit;
using RawRabbit.Context;
using WebStations.Models;

namespace WebStations.Controllers
{
    public class StationController : Controller
    {
        private const string _keyCache = "station";
        private readonly IBusClient _busClient;
        private readonly IDistributedCache _cache;
        private readonly ILogger<StationController> _logger;
        private readonly ITitleStationDb _titleStationDb;
        private bool firstLoad;

        public StationController(ILogger<StationController> logger,
            ITitleStationDb titleStationDb,
            IDistributedCache cache,
            IBusClient busClient)
        {
            _titleStationDb = titleStationDb;
            _cache = cache;
            _logger = logger;
            _busClient = busClient;
            _busClient.SubscribeAsync<UpdateInfoStationsEvent>(UpdateInfoStation);
        }

        private async Task UpdateInfoStation(UpdateInfoStationsEvent @event, IMessageContext message)
        {
            var model = await GetStations();

            await UpdateCache(model);
        }


        public async Task<IActionResult> Index()
        {
            try
            {
                var modelCache = await _cache.GetAsync(_keyCache);
                if (firstLoad && modelCache != null)
                {
                    var serialized = Encoding.UTF8.GetString(modelCache);

                    return View(JsonConvert.DeserializeObject<IEnumerable<StationViewModel>>(serialized));
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"{ex} Cache Redis doesn't work.");
            }

            var model = await GetStations();

            await UpdateCache(model);
            firstLoad = true;
            return View(model);
        }


        private async Task<List<StationViewModel>> GetStations()
        {
            List<StationViewModel> stations = new List<StationViewModel>();
            try
            {
                var values = await _titleStationDb.GetTitleStationAsync(10);

                if (values != null)
                {
                    foreach (var value in values)
                    {
                        stations.Add(new StationViewModel
                            {
                                Code = value.Code,
                                Name = value.Name,
                                NameRailway = value.NameRailway,
                                DateUpdate = value.DateUpdate
                            }
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"{ex}");
            }

            return stations;
        }

        private async Task UpdateCache(List<StationViewModel> stations)
        {
            DistributedCacheEntryOptions userExpire = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(3600)
            };

            var json = JsonConvert.SerializeObject(stations);
            var jsonByte = Encoding.UTF8.GetBytes(json);

            try
            {
                await _cache.SetAsync(_keyCache, jsonByte, userExpire);
            }
            catch
            {
                _logger.LogWarning("Cache Redis doesn't work.");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}