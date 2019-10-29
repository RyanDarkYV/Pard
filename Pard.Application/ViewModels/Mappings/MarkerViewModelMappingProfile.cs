using AutoMapper;
using Pard.Domain.Entities.Locations;

namespace Pard.Application.ViewModels.Mappings
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