using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HttpClients.Interfaces;
using JsonModels;

namespace HttpClients
{
    public class TitleStationClient : BaseClient, ITitleStationDb 
    {
        public TitleStationClient(string baseAddress) : base(baseAddress)
        {
            ServiceAddress = "api/stations";
        }

        protected override string ServiceAddress { get; }
        public async Task<IEnumerable<TitleStationJsonModel>> GetTitleStationAsync(int number)
        {
            return await this.GetAsync<List<TitleStationJsonModel>>($"{this.ServiceAddress}/{number}");
        }
    }
}
