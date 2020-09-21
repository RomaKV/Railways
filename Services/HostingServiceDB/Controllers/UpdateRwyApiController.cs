using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HttpClients.Interfaces;
using JsonModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostingServiceDB.Controllers
{
    [Produces("application/json")]
    [Route("api/update")]
    [ApiController]
    public class RailwayApiController : ControllerBase, IUpdateRwyDb
    {

        private readonly IUpdateRwyDb _updateRwyDb;
       
        public RailwayApiController(IUpdateRwyDb updateRwyDb)
        {
            _updateRwyDb = updateRwyDb;
        }


        [HttpPost("railways"), ActionName("Post")]
        public async Task<HttpResponseMessage> UpdateRailwayAsync(IEnumerable<RailwayJsonModel> railways)
        {
            return await _updateRwyDb.UpdateRailwayAsync(railways);
        }

        [HttpPost("stations"), ActionName("Post")]
        public async Task<HttpResponseMessage> UpdateStationAsync(IEnumerable<StationJsonModel> stations)
        {
            return await _updateRwyDb.UpdateStationAsync(stations);
        }
    }
}
