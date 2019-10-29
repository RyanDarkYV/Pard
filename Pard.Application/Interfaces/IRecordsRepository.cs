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
        Task<Record> GetRecord(string title, Guid userId);
        Task<IEnumerable<Record>> GetAllRecordsForUser(Guid userId);
        Task<IEnumerable<Record>> GetUnfinishedRecordsForUser(Guid userId);
    }
}
