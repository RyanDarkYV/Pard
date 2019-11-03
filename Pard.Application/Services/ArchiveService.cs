using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Pard.Application.Common.Interfaces;
using Pard.Application.ViewModels;
using Pard.Domain.Entities.Records;

namespace Pard.Application.Services
{
    public class ArchiveService : IArchiveService
    {
        private readonly IMapper _mapper;
        private readonly IRecordsRepository _repository;

        public ArchiveService(IRecordsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RecordViewModel>> GetArchivedRecords(Guid userId)
        {
            var records = await _repository.GetArchivedRecordsForUser(userId);
            var result = _mapper.Map<IEnumerable<Record>, IEnumerable<RecordViewModel>>(records);

            return result;

        }

        public async Task DeleteRecord(Guid recordId, Guid userId)
        {
            await _repository.Delete(recordId, userId);
        }

        public async Task RestoreRecord(Guid recordId, Guid userId)
        {
            await _repository.Restore(recordId, userId);
        }
    }
}