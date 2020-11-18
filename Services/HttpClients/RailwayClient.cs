using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HttpClients.Interfaces;
using JsonModels;
using Microsoft.Extensions.Configuration;

namespace HttpClients
{
    public class RailwayClient : BaseClient,  IRailwayInfo
    {
        
        public RailwayClient(string baseAddress) : base(baseAddress)
        {
            ServiceAddress = "api/info/railway";
        }

        protected override string ServiceAddress { get; }
       
        
        public async Task<ListRailwaysJsonModel> GetAllAsync()
        {
            return await this.GetAsync<ListRailwaysJsonModel>($"{this.ServiceAddress}/getall");
      
        }

        public async Task<ListRailwayDateJsonModel> GetAllgetDateAsync(DateTime dateCreate, DateTime dateUpdate)
        {
            string create = dateCreate.ToString("YYYY-MM-DDThh:mm:ss");
            string update = dateUpdate.ToString("YYYY-MM-DDThh:mm:ss");
            return await this.GetAsync<ListRailwayDateJsonModel>
                   ($"{this.ServiceAddress}/getUpdate?dateCreate={create}&dateUpdate={update}");
        }
    }
}
