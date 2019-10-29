using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pard.Application.ViewModels;

namespace Pard.Application.Services
{
    public interface IRecordsService
    {
        Task CreateRecord(RecordViewModel model);
        Task UpdateRecord(RecordViewModel model);
        Task<RecordViewModel> GetRecord(string title, Guid userId);
        Task<IEnumerable<RecordViewModel>> GetAllRecordForUser(Guid userId);
        Task<IEnumerable<RecordViewModel>> GetAllRecordInWorkForUser(Guid userId);
    }
}