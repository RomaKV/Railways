using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HttpClients.Interfaces;
using JsonModels;

namespace HttpClients
{
    public class UpdateRwyDbClient :  BaseClient, IUpdateRwyDb
    {
        public UpdateRwyDbClient(string baseAddress) : base(baseAddress)
        {
            ServiceAddress = "api/update";
        }

        protected override string ServiceAddress { get; }
        
        public async Task<HttpResponseMessage> UpdateRailwayAsync(IEnumerable<RailwayJsonModel> railways)
        {
            return await PostAsync($"{this.ServiceAddress}/railways", railways);
        }

        public async Task<HttpResponseMessage> UpdateStationAsync(IEnumerable<StationJsonModel> stations)
        {
            return await PostAsync($"{this.ServiceAddress}/stations", stations);
        }
    }
}
