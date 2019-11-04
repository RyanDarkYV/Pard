using AutoMapper;
using MediatR;
using Pard.Application.Common.Abstractions.Commands;
using Pard.Application.ViewModels;
using Pard.Domain.Entities.Records;
using System;
using System.Threading;
using System.Threading.Tasks;
using Pard.Application.Common.Interfaces;

namespace Pard.Application.Records.Commands.CreateRecord
{
    public class CreateRecordCommandHandler : ICommandHandler<CreateRecordCommand>
    {
        private readonly IRecordsRepository _repository;
        private readonly IMapper _mapper;

        public CreateRecordCommandHandler(IRecordsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateRecordCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateRecordCommand, Record>(request);
            entity.Id = Guid.NewGuid();
            entity.AddedAt = DateTime.UtcNow;
            entity.Location.RecordId = entity.Id;
            await _repository.Create(entity);

            return Unit.Value;
        }
    }
}