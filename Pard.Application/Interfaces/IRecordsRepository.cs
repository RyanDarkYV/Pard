using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pard.Domain.Entities.Records;

namespace Pard.Application.Interfaces
{
    public interface IRecordsRepository
    {
        Task Create(Record model);
        Task Update(Record model);
        Task<Record> GetRecordByTitle(string title, Guid userId);
        Task<Record> GetRecordById(Guid id, Guid userId);
        Task<IEnumerable<Record>> GetAllFinishedRecordsForUser(Guid userId);
        Task<IEnumerable<Record>> GetUnfinishedRecordsForUser(Guid userId);
        Task<IEnumerable<Record>> GetArchivedRecordsForUser(Guid userId);
        Task SoftDelete(Guid recordId, Guid userId);
        Task Delete(Guid recordId, Guid userId);
        Task Restore(Guid recordId, Guid userId);
        Task<IEnumerable<Record>> GetRecords(bool status, Guid userId);
    }
}
