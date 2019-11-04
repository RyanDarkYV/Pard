using System;
using AutoMapper;
using Pard.Application.Records.Commands.CreateRecord;
using Pard.Domain.Entities.Records;

namespace Pard.Application.Common.Mappings
{
    public class CreateRecordCommandProfile : Profile
    {
        public CreateRecordCommandProfile()
        {
            CreateMap<CreateRecordCommand, Record>()
                .ForMember(record => record.Id,
                record => record.MapFrom(recordvm => Guid.Parse(recordvm.Id)));
        }
    }
}