using JsonModels;
using System;
using System.Threading.Tasks;

namespace HttpClients.Interfaces
{
    public interface IStationInfo
    {

        Task<ListStationsJsonModel> GetAllAsync();

        Task<ListStationsDateJsonModel> GetAllgetDateAsync(DateTime dateCreate, DateTime dateUpdate);
    }
}