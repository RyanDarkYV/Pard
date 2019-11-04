using AutoMapper;
using Pard.Domain.Entities.Locations;
using Pard.Application.ViewModels;

namespace Pard.Application.Common.Mappings
{
    public class MarkerViewModelMappingProfile : Profile
    {
        public MarkerViewModelMappingProfile()
        {
            CreateMap<Location, MarkerViewModel>()
                .ForMember(marker => marker.Latitude, marker => marker.MapFrom(location => location.Latitude))
                .ForMember(marker => marker.Longitude, marker => marker.MapFrom(location => location.Longitude));
        }
    }
}