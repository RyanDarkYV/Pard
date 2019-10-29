using System;
using AutoMapper;
using Pard.Domain.Entities.Locations;

namespace Pard.Application.ViewModels.Mappings
{
    public class LocationViewModelMappingProfile : Profile
    {
        public LocationViewModelMappingProfile()
        {
            CreateMap<LocationViewModel, Location>()
                .ForMember(location => location.Id,
                    location => location.MapFrom(locationvm => Guid.Parse(locationvm.Id)))
                .ForMember(location => location.RecordId,
                    location => location.MapFrom(locationvm => Guid.Parse(locationvm.RecordId)))
                .ForMember(location => location.Latitude,
                    location => location.MapFrom(locationvm => locationvm.Latitude))
                .ForMember(location => location.Longitude,
                    location => location.MapFrom(locationvm => locationvm.Longitude))
                .ForMember(location => location.AddressCity,
                    location => location.MapFrom(locationvm => locationvm.AddressCity))
                .ForMember(location => location.AddressCountry,
                    location => location.MapFrom(locationvm => locationvm.AddressCountry))
                .ForMember(location => location.AddressStreet,
                    location => location.MapFrom(locationvm => locationvm.AddressStreet))
                .ForMember(location => location.AddressState,
                    location => location.MapFrom(locationvm => locationvm.AddressState))
                .ForMember(location => location.RecordId,
                    location => location.MapFrom(locationvm => locationvm.RecordId))
                .ReverseMap()
                .ForMember(x => x.Viewport, opt => opt.Ignore())
                .ForPath(x => x.Marker.Latitude,
                    x => x.MapFrom(p => p.Latitude))
                .ForPath(x => x.Marker.Longitude,
                    x => x.MapFrom(p => p.Longitude));
        }
    }
}