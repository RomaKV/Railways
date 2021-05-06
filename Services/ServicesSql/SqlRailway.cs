using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EntitySql;
using EntitySql.Entities;
using HttpClients.Interfaces;
using JsonModels;
using Microsoft.EntityFrameworkCore;

namespace ServicesSql
{
    public class SqlRailway : IUpdateRwyDb
    {
        private readonly RailwayContext _context;

        public SqlRailway(RailwayContext context)
        {
            _context = context;
        }

        public async Task<HttpResponseMessage> UpdateRailwayAsync(IEnumerable<RailwayJsonModel> railways)
        {
            if (railways == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            try
            {
                bool isUpdated = false;
                List<Railway> railwaysNew = new List<Railway>();

                foreach (var railway in railways)
                {
                    var value = await _context.Railways.SingleOrDefaultAsync(r => r.Id == railway.ID);

                    if (value != null) // update
                    {
                        if (railway.DateUpdate > value.DateUpdate)
                        {
                            continue;
                        }

                        value.ShortName = railway.ShortName;
                        value.Name = railway.Name;
                        value.TelegraphName = railway.TelegraphName;
                        value.CountryId = railway.CountryID;
                        value.DateCreate = railway.DateCreate;
                        value.DateUpdate = railway.DateUpdate;
                        value.Code = railway.Code;
                        _context.Entry(value).State = EntityState.Modified;
                        isUpdated = true;
                    }
                    else // new
                    {
                        railwaysNew.Add(new Railway
                        {
                            Id = railway.ID,
                            Code = railway.Code,
                            Name = railway.Name,
                            ShortName = railway.ShortName,
                            TelegraphName = railway.TelegraphName,
                            DateCreate = railway.DateCreate,
                            DateUpdate = railway.DateUpdate,
                            CountryId = railway.CountryID
                        });
                    }
                }

                if (isUpdated)
                {
                    await _context.SaveChangesAsync();
                }

                if (railwaysNew.Count > 0)
                {
                    await _context.Railways.AddRangeAsync(railwaysNew);

                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }


        public async Task<HttpResponseMessage> UpdateStationAsync(IEnumerable<StationJsonModel> stations)
        {
            if (stations == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            try
            {
                bool isUpdated = false;

                List<Station> stationsNew = new List<Station>();

                foreach (var station in stations)
                {
                    var value = await _context.Stations.SingleOrDefaultAsync(r => r.Code == station.Code);

                    if (value != null) // update
                    {
                        if (station.DateUpdate > value.DateUpdate)
                        {
                            continue;
                        }

                        value.Name = station.Name;
                        value.RailwayId = station.RailwayID;
                        value.CountryId = station.CountryID;
                        value.DateCreate = station.DateCreate;
                        value.DateUpdate = station.DateUpdate;
                        value.Code = station.Code;
                        value.CodeOSGD = station.CodeOSGD;
                        value.Name12Char = station.Name12Char;
                        value.FreightSign = station.FreightSign;
                        value.RailwayDepartmentID = station.RailwayDepartmentID;
                        value.CountryId = station.CountryID;
                        _context.Entry(value).State = EntityState.Modified;
                        isUpdated = true;
                    }
                    else // new
                    {
                        stationsNew.Add(new Station
                        {
                            Id = station.ID,
                            Name = station.Name,
                            RailwayId = station.RailwayID,
                            CountryId = station.CountryID,
                            DateCreate = station.DateCreate,
                            DateUpdate = station.DateUpdate,
                            Code = station.Code,
                            CodeOSGD = station.CodeOSGD,
                            Name12Char = station.Name12Char,
                            FreightSign = station.FreightSign,
                            RailwayDepartmentID = station.RailwayDepartmentID
                        });
                    }
                }

                if (isUpdated)
                {
                    await _context.SaveChangesAsync();
                }

                if (stationsNew.Count > 0)
                {
                    await _context.BulkInsertAsync(stationsNew);

                    await _context.BulkSaveChangesAsync(bulk => bulk.BatchSize = 100);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }


        public async Task<HttpResponseMessage> AddNewAsync(RailwayJsonModel model)
        {
            if (model == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            var railway = new Railway
            {
                Id = model.ID,
                Code = model.Code,
                Name = model.Name,
                ShortName = model.ShortName,
                TelegraphName = model.TelegraphName,
                DateCreate = model.DateCreate,
                DateUpdate = model.DateUpdate,
                CountryId = model.CountryID
            };

            await _context.AddAsync(railway);

            await _context.SaveChangesAsync();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> AddNewAsync(IEnumerable<RailwayJsonModel> models)
        {
            if (models == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            foreach (var model in models)
            {
                var railway = new Railway
                {
                    Id = model.ID,
                    Code = model.Code,
                    Name = model.Name,
                    ShortName = model.ShortName,
                    TelegraphName = model.TelegraphName,
                    DateCreate = model.DateCreate,
                    DateUpdate = model.DateUpdate,
                    CountryId = model.CountryID
                };

                await _context.AddAsync(railway);
            }

            var res = await _context.SaveChangesAsync();

            if (res > 0)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        public Task<HttpResponseMessage> Delete(int id)
        {
            _context.Remove(id);

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }


        public async Task<HttpResponseMessage> UpdateAsync(int id, RailwayJsonModel railway)
        {
            var value = await _context.Railways.AsNoTracking().SingleAsync(r => r.Id == id);

            if (value != null)
            {
                value.ShortName = railway.ShortName;
                value.TelegraphName = railway.TelegraphName;
                value.Name = railway.Name;
                value.DateCreate = railway.DateCreate;
                value.DateUpdate = railway.DateUpdate;
                value.Code = railway.Code;
            }

            var res = await _context.SaveChangesAsync();

            if (res > 0)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        public async Task<IEnumerable<RailwayJsonModel>> GetAllAsync()
        {
            List<RailwayJsonModel> result = new List<RailwayJsonModel>();

            var values = await _context.Railways.AsNoTracking().ToListAsync();

            if (values != null)
            {
                foreach (var value in values)
                {
                    result.Add(new RailwayJsonModel
                    {
                        Name = value.Name,
                        ShortName = value.ShortName,
                        Code = value.Code,
                        CountryID = value.CountryId,
                        TelegraphName = value.TelegraphName,
                        ID = value.Id,
                        DateUpdate = value.DateUpdate,
                        DateCreate = value.DateCreate
                    });
                }
            }

            return result;
        }

        public async Task<RailwayJsonModel> GetByIdAsync(int id)
        {
            var value = await _context.Railways.AsNoTracking().SingleAsync(r => r.Id == id);

            if (value != null)
            {
                return new RailwayJsonModel
                {
                    Name = value.Name,
                    ShortName = value.ShortName,
                    Code = value.Code,
                    CountryID = value.CountryId,
                    TelegraphName = value.TelegraphName,
                    ID = value.Id,
                    DateUpdate = value.DateUpdate,
                    DateCreate = value.DateCreate
                };
            }

            return new RailwayJsonModel();
        }
    }
}