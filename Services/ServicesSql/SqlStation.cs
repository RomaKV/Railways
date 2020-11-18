using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntitySql;
using HttpClients.Interfaces;
using JsonModels;
using Microsoft.EntityFrameworkCore;

namespace SqlServices
{
    public class SqlStation : ITitleStationDb
    {
        private readonly RailwayContext _context;

        public SqlStation(RailwayContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<TitleStationJsonModel>> GetTitleStationAsync(int number)
        {
            return await _context.Stations.AsNoTracking()
                .OrderByDescending(d => d.DateUpdate)
                .Take(number)
                .Include(r => r.Railway)
                .Select(s => new TitleStationJsonModel
                {
                    Code = s.Code,
                    DateUpdate = s.DateUpdate,
                    Name = s.Name,
                    NameRailway = s.Railway.Name ?? string.Empty
                })
                .OrderBy(d => d.DateUpdate)
                .ToListAsync();
        }
    }
}