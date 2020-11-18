using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpClients.Interfaces;
using JsonModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostingServiceDB.Controllers
{
    [Produces("application/json")]
    [Route("api/stations")]
    [ApiController]
    public class StationApiController : ControllerBase, ITitleStationDb
    {
        private readonly ITitleStationDb _titleStationDb;
        
        public StationApiController(ITitleStationDb titleStationDb)
        {
            _titleStationDb = titleStationDb;
        }

        [HttpGet, ActionName("Get")]
        public JsonResult Get()
        {
            return new JsonResult($"It's work! [{DateTime.Now}]");
        }

        [HttpGet("{number}"), ActionName("Get")]
        public async Task<IEnumerable<TitleStationJsonModel>> GetTitleStationAsync(int number)
        {
           return await  _titleStationDb.GetTitleStationAsync(number);
        }
    }
}
