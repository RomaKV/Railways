using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using HttpClients.Interfaces;
using JsonModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebStations.Models;

namespace WebStations.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITitleStationDb _titleStationDb;
        private readonly IDistributedCache _cache;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ITitleStationDb titleStationDb, IDistributedCache cache)
        {
            _titleStationDb = titleStationDb;
            _cache = cache;
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            string keyCache = "station";

            try
            {
                var modelCache = await _cache.GetAsync(keyCache);
                if (modelCache != null)
                {
                    var serialized = Encoding.UTF8.GetString(modelCache);

                    return View(JsonConvert.DeserializeObject<IEnumerable<StationViewModel>>(serialized));
                }
            }
            catch
            {
               _logger.LogWarning("Cache Redis doesn't work.");
            }
            
            List<StationViewModel> model = new List<StationViewModel>();

            BinaryFormatter formatter = new BinaryFormatter();

            var values = await _titleStationDb.GetTitleStationAsync(10);

            if (values != null)
            {
                foreach (var value in values)
                {
                    model.Add(new StationViewModel 
                        {
                          Code = value.Code,
                          Name = value.Name,
                          NameRailway = value.NameRailway,
                          DateUpdate =  value.DateUpdate
                        }
                    );
                }
            }

            DistributedCacheEntryOptions userExpire = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(600)
            };

            var json = JsonConvert.SerializeObject(model);
            var jsonByte = Encoding.UTF8.GetBytes(json);

            try
            {
                await _cache.SetAsync(keyCache, jsonByte, userExpire);
            }
            catch
            {
                _logger.LogWarning("Cache Redis doesn't work.");

            }
            
            return View(model);
        }



       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
