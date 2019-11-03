using AutoMapper;
using Pard.Application.ViewModels;
using Pard.Domain.Entities.Records;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pard.Application.Common.Interfaces;

namespace Pard.Application.Services
{
    public class RecordsService : IRecordsService
    {
        private readonly IRecordsRepository _repository;
        private readonly IMapper _mapper;

        public RecordsService(IRecordsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateRecord(RecordViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.AddedAt = DateTime.UtcNow;
            model.Location.RecordId = model.Id;
            var entity = _mapper.Map<Record>(model);
            await _repository.Create(entity);
        }

        public async Task UpdateRecord(RecordViewModel model)
        {
            var entity = _mapper.Map<Record>(model);
            if (entity.IsDone)
            {
                if (entity.FinishedAt == null)
                {
                    entity.FinishedAt = DateTime.UtcNow;
                }
            }

            await _repository.Update(entity);
        }

        public async Task<RecordViewModel> GetRecordById(Guid recordId, Guid userId)
        {
            var entity = await _repository.GetRecordById(recordId, userId);
            var result = _mapper.Map<RecordViewModel>(entity);
            var marker = _mapper.Map<MarkerViewModel>(entity.Location);
            result.Location.Marker = marker;
            return result;
        }

        public async Task<RecordViewModel> GetRecordByTitle(string title, Guid userId)
        {
            var entity = await _repository.GetRecordByTitle(title, userId);
            var result = _mapper.Map<RecordViewModel>(entity);
            var marker = _mapper.Map<MarkerViewModel>(entity.Location);
            result.Location.Marker = marker;
            return result;
        }

        public async Task<IEnumerable<RecordViewModel>> GetAllFinishedRecordsForUser(Guid userId)
        {
            var entitiesList = await _repository.GetAllFinishedRecordsForUser(userId);
            IEnumerable<RecordViewModel> resultList = 
                _mapper.Map<IEnumerable<Record>, IEnumerable<RecordViewModel>>(entitiesList);

            return resultList;
        }

        public async Task<IEnumerable<RecordViewModel>> GetAllRecordInWorkForUser(Guid userId)
        {
            var records = await _repository.GetUnfinishedRecordsForUser(userId);
            IEnumerable<RecordViewModel> resultList =
                _mapper.Map<IEnumerable<Record>, IEnumerable<RecordViewModel>>(records);

            return resultList;
        }

        public async Task SoftDeleteRecord(Guid recordId, Guid userId)
        {
            await _repository.SoftDelete(recordId, userId);
        }
    }
}