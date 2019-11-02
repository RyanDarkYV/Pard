using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pard.Application.Interfaces;
using Pard.Domain.Entities.Records;

namespace Pard.Application.Repositories.Records
{
    public class SqlServerRecordsRepository : IRecordsRepository
    {
        // TODO: Add abstraction for RecordsContext to ApplicationLayer and move repo implementation to ApplicationLayer
        private readonly IRecordsContext _context;

        public SqlServerRecordsRepository(IRecordsContext context)
        {
            _context = context;
        }

        public async Task Create(Record model)
        {
            await _context.Records.AddAsync(model);
            await _context.Locations.AddAsync(model.Location);
            await _context.SaveChangesAsync(new CancellationToken());
        }

        public async Task Update(Record model)
        {
            Record entity = await _context.Records
                .Where(x => x.Id == model.Id)
                .Include(x => x.Location)
                .FirstOrDefaultAsync();
           
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

            await _context.SaveChangesAsync(new CancellationToken());
        }
        
        public async Task<Record> GetRecordById(Guid id, Guid userId)
        {
            var result = await _context.Records
                .Where(x => x.UserId == userId && x.Id == id && x.IsDeleted == false)
                .Include(x => x.Location)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<Record> GetRecordByTitle(string title, Guid userId)
        {
            var result = await _context.Records
                .Where(x => x.UserId == userId && x.Title == title && x.IsDeleted == false)
                .Include(x => x.Location)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Record>> GetAllFinishedRecordsForUser(Guid userId)
        {
            var result = _context.Records
                .Where(x => x.UserId == userId && x.IsDeleted == false && x.IsDone)
                .Include(x => x.Location)
                .OrderBy(x => x.AddedAt);
            return result;
        }

        public async Task<IEnumerable<Record>> GetUnfinishedRecordsForUser(Guid userId)
        {
            var result = _context.Records
                .Where(x => x.UserId == userId && x.IsDone == false && x.IsDeleted == false)
                .Include(x => x.Location)
                .OrderBy(x => x.AddedAt);
            return result;
        }

        public async Task<IEnumerable<Record>> GetArchivedRecordsForUser(Guid userId)
        {
            var result = _context.Records
                .Where(x => x.UserId == userId && x.IsDeleted)
                .Include(x => x.Location)
                .OrderBy(x => x.AddedAt);
            return result;
        }

        public async Task SoftDelete(Guid recordId, Guid userId)
        {
            var result = await _context.Records
                .Where(x => x.UserId == userId && x.Id == recordId && x.IsDeleted == false)
                .FirstOrDefaultAsync();
            if (result == null)
            {
                return;
            }

            result.IsDeleted = true;
            await _context.SaveChangesAsync(new CancellationToken());
        }

        public async Task Delete(Guid recordId, Guid userId)
        {
            var result = await _context.Records
                .Where(x => x.UserId == userId && x.Id == recordId && x.IsDeleted)
                .FirstOrDefaultAsync();
            if (result == null)
            {
                return;
            }

            _context.Records.Remove(result);
            await _context.SaveChangesAsync(new CancellationToken());
        }

        public async Task Restore(Guid recordId, Guid userId)
        {
            var result = await _context.Records
                .Where(x => x.UserId == userId && x.Id == recordId && x.IsDeleted)
                .FirstOrDefaultAsync();
            if (result == null)
            {
                return;
            }

            result.IsDeleted = false;
            await _context.SaveChangesAsync(new CancellationToken());
        }
    }
}