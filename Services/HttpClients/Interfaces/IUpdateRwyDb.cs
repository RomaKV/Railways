using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JsonModels;

namespace HttpClients.Interfaces
{
    public interface IUpdateRwyDb
    {
        Task<HttpResponseMessage> UpdateRailwayAsync(IEnumerable<RailwayJsonModel> railways);

        Task<HttpResponseMessage> UpdateStationAsync(IEnumerable<StationJsonModel> stations);
    }
}