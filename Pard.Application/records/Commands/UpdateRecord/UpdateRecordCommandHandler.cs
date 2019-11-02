using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Pard.Application.Common.Abstractions.Commands;
using Pard.Application.Interfaces;
using Pard.Application.ViewModels;
using Pard.Domain.Entities.Records;

namespace Pard.Application.Records.Commands.UpdateRecord
{
    public class UpdateRecordCommandHandler : ICommandHandler<UpdateRecordCommand>
    {
        private readonly IRecordsRepository _repository;
        private readonly IMapper _mapper;

        public UpdateRecordCommandHandler(IRecordsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateRecordCommand request, CancellationToken cancellationToken)
        {
            var vm = _mapper.Map<UpdateRecordCommand, RecordViewModel>(request);
            var entity = _mapper.Map<RecordViewModel, Record>(vm);
            if (entity.IsDone)
            {
                if (entity.FinishedAt == null)
                {
                    entity.FinishedAt = DateTime.UtcNow;
                }
                else
                {
                    entity.FinishedAt = null;
                }
            }
            await _repository.Update(entity);

            return Unit.Value;
        }
    }
}