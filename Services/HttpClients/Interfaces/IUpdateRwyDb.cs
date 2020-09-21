using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EntitySql.Entities;
using JsonModels;
using Microsoft.AspNetCore.Identity;

namespace HttpClients.Interfaces
{
    public interface IUpdateRwyDb
    {
        Task<HttpResponseMessage> UpdateRailwayAsync(IEnumerable<RailwayJsonModel> railways);

        Task<HttpResponseMessage> UpdateStationAsync(IEnumerable<StationJsonModel> stations);

       // Task<HttpResponseMessage> AddNewAsync(IEnumerable<RailwayJsonModel> model);

       // Task<HttpResponseMessage> Delete(int id);

       //Task<IEnumerable<RailwayJsonModel>> GetAllAsync();

       //Task<RailwayJsonModel> GetByIdAsync(int id);

       //Task<HttpResponseMessage> UpdateAsync(int id, RailwayJsonModel railway);


    }
}
