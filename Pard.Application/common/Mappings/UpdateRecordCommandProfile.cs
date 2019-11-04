using System;
using AutoMapper;
using Pard.Application.Records.Commands.UpdateRecord;
using Pard.Domain.Entities.Records;

namespace Pard.Application.Common.Mappings
{
    public class UpdateRecordCommandProfile : Profile
    {
        public UpdateRecordCommandProfile()
        {
            CreateMap<UpdateRecordCommand, Record>()
                .ForMember(record => record.Id,
                    record => record.MapFrom(recordvm => Guid.Parse(recordvm.Id)));
        }   
    }
}