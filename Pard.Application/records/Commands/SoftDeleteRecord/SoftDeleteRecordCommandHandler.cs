using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Pard.Application.Common.Abstractions.Commands;
using Pard.Application.Common.Interfaces;

namespace Pard.Application.Records.Commands.SoftDeleteRecord
{
    public class SoftDeleteRecordCommandHandler : ICommandHandler<SoftDeleteRecordCommand>
    {
        private readonly IRecordsRepository _repository;

        public SoftDeleteRecordCommandHandler(IRecordsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(SoftDeleteRecordCommand request, CancellationToken cancellationToken)
        {
            await _repository.SoftDelete(request.Id, request.UserId);

            return Unit.Value;
        }
    }
}