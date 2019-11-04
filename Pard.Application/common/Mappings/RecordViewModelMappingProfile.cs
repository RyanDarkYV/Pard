using System;
using AutoMapper;
using Pard.Application.ViewModels;
using Pard.Domain.Entities.Records;

namespace Pard.Application.Common.Mappings
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