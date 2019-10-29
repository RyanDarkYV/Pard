using System;
using AutoMapper;
using Pard.Domain.Entities.Records;

namespace Pard.Application.ViewModels.Mappings
{
    public class RecordViewModelMappingProfile : Profile
    {
        public RecordViewModelMappingProfile()
        {
            CreateMap<RecordViewModel, Record>()
                .ForMember(record => record.Id,
                    record => record.MapFrom(recordvm => Guid.Parse(recordvm.Id)))
                .ReverseMap();
        }
    }
}