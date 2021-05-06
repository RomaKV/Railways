using JsonModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Interfaces
{
    public interface IRailwayInfo
    {
        Task<ListRailwaysJsonModel> GetAllAsync();

        Task<ListRailwayDateJsonModel> GetAllgetDateAsync(DateTime dateCreate, DateTime dateUpdate);

    }
}
