using HttpClients.Interfaces;
using JsonModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients
{
    public class StationClient : BaseClient, IStationInfo
    {
        public StationClient(string baseAddress) : base(baseAddress)
        {
            ServiceAddress = "api/info/station";
        }

        protected override string ServiceAddress { get; }


        public async Task<ListStationsJsonModel> GetAllAsync()
        {
            return await this.GetAsync<ListStationsJsonModel>($"{this.ServiceAddress}/getall");

        }



        public async Task<ListStationsDateJsonModel> GetAllgetDateAsync(DateTime dateCreate, DateTime dateUpdate)
        {
            string create = dateCreate.ToString("YYYY-MM-DDThh:mm:ss");
            string update = dateUpdate.ToString("YYYY-MM-DDThh:mm:ss");
            return await this.GetAsync<ListStationsDateJsonModel>
                ($"{this.ServiceAddress}/getUpdate?dateCreate={create}&dateUpdate={update}");
        }

    }
}
