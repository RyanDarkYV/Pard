using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pard.Application.ViewModels;

namespace Pard.Application.Common.Interfaces
{
    public interface IRecordsService
    {
        Task CreateRecord(RecordViewModel model);
        Task UpdateRecord(RecordViewModel model);
        Task<RecordViewModel> GetRecordById(Guid recordId, Guid userId);
        Task<RecordViewModel> GetRecordByTitle(string title, Guid userId);
        Task<IEnumerable<RecordViewModel>> GetAllFinishedRecordsForUser(Guid userId);
        Task<IEnumerable<RecordViewModel>> GetAllRecordInWorkForUser(Guid userId);
        Task SoftDeleteRecord(Guid recordId, Guid userId);
    }
}