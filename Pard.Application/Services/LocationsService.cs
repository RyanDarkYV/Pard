using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Pard.Application.Common.Interfaces;
using Pard.Application.ViewModels;
using Pard.Domain.Entities.Locations;

namespace Pard.Application.Services
{
    public class LocationsService : ILocationsService
    {
        private readonly ILocationsRepository _repository;
        private readonly IMapper _mapper;
        public LocationsService(ILocationsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LocationViewModel>> GetLocationsForActiveRecords(Guid userId)
        {
            var locations = await _repository.GetLocationsForActiveRecords(userId);
            IEnumerable<LocationViewModel> result = 
                _mapper.Map<IEnumerable<Location>, IEnumerable<LocationViewModel>>(locations);

            return result;
        }
    }
}