using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JsonModels;

namespace HttpClients.Interfaces
{
   public interface ITitleStationDb
   {
        Task<IEnumerable<TitleStationJsonModel>> GetTitleStationAsync(int number);
   }
}
