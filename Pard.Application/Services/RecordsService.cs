using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using Pard.Application.Interfaces;
using Pard.Application.ViewModels;
using Pard.Domain.Entities.Locations;
using Pard.Domain.Entities.Records;

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
            Console.WriteLine(JsonConvert.SerializeObject(entity));
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

        public async Task<RecordViewModel> GetRecord(string title, Guid userId)
        {
            var entity = await _repository.GetRecord(title, userId);
            //var id = Guid.NewGuid();
            //var entity = new Record
            //{
            //    Title = "test",
            //    Description = "test",
            //    Id = id,
            //    UserId = userId,
            //    AddedAt = DateTime.UtcNow,
            //    FinishedAt = DateTime.UtcNow,
            //    IsDone = true,
            //    Location = new Location
            //    {
            //        Longitude = 30.444187000000056,
            //        Latitude = 50.445151,
            //        AddressCity = "Kyiv",
            //        AddressState = null,
            //        AddressCountry = "Ukraine",
            //        AddressStreet = "Borshchahivska St, 154",
            //        RecordId = id
            //    }
            //};
            var result = _mapper.Map<RecordViewModel>(entity);
            var marker = _mapper.Map<MarkerViewModel>(entity.Location);
            result.Location.Marker = marker;
            return result;
        }

        public async Task<IEnumerable<RecordViewModel>> GetAllRecordForUser(Guid userId)
        {
            var entitiesList = await _repository.GetAllRecordsForUser(userId);
            IEnumerable<RecordViewModel> resultList = _mapper.Map<IEnumerable<Record>, IEnumerable<RecordViewModel>>(entitiesList);

            return resultList;
        }

        public async Task<IEnumerable<RecordViewModel>> GetAllRecordInWorkForUser(Guid userId)
        {
            var records = await _repository.GetUnfinishedRecordsForUser(userId);
            IEnumerable<RecordViewModel> resultList =
                _mapper.Map<IEnumerable<Record>, IEnumerable<RecordViewModel>>(records);

            return resultList;
        }
    }
}