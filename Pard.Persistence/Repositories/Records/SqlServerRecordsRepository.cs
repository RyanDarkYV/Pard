using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pard.Application.Interfaces;
using Pard.Domain.Entities.Locations;
using Pard.Domain.Entities.Records;
using Pard.Persistence.Contexts;

namespace Pard.Persistence.Repositories.Records
{
    public class SqlServerRecordsRepository : IRecordsRepository
    {
        private readonly RecordsContext _context;

        public SqlServerRecordsRepository(RecordsContext context)
        {
            _context = context;
        }

        public async Task Create(Record model)
        {
            await _context.Records.AddAsync(model);
            await _context.Locations.AddAsync(model.Location);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Record model)
        {
            Record entity = await _context.Records.Where(x => x.Id == model.Id).Include(x => x.Location).FirstOrDefaultAsync();
            //Location locationEntity =
                //await _context.Locations.Where(x => x.RecordId == entity.Id).FirstOrDefaultAsync();
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.IsDone = model.IsDone;
            entity.FinishedAt = model.FinishedAt;
            entity.UserId = model.UserId;
            entity.AddedAt = entity.AddedAt;
            entity.Id = model.Id;
            entity.Location.Latitude = model.Location.Latitude;
            entity.Location.Longitude = model.Location.Longitude;
            entity.Location.AddressCity = model.Location.AddressCity;
            entity.Location.AddressCountry = model.Location.AddressCountry;
            entity.Location.AddressState = model.Location.AddressState;
            entity.Location.AddressStreet = model.Location.AddressStreet;
            entity.Location.Id = model.Location.Id;
            entity.Location.RecordId = model.Location.RecordId;

            await _context.SaveChangesAsync();
        }

        public async Task<Record> GetRecord(string title, Guid userId)
        {
            var result = await _context.Records.Where(x => x.UserId == userId && x.Title == title).Include(x => x.Location).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Record>> GetAllRecordsForUser(Guid userId)
        {
            var result = _context.Records.Where(x => x.UserId == userId).Include(x => x.Location);
            return result;
        }

        public async Task<IEnumerable<Record>> GetUnfinishedRecordsForUser(Guid userId)
        {
            var result = _context.Records.Where(x => x.UserId == userId && x.IsDone == false).Include(x => x.Location);
            return result;
        }
    }
}