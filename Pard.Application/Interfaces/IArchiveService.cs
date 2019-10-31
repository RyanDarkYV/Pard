using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pard.Application.ViewModels;

namespace Pard.Application.Interfaces
{
    public interface IArchiveService
    {
        Task<IEnumerable<RecordViewModel>> GetArchivedRecords(Guid userId);
        Task DeleteRecord(Guid recordId, Guid userId);
        Task RestoreRecord(Guid recordId, Guid userId);
    }
}