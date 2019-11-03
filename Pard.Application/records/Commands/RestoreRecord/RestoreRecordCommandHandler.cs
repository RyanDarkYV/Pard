using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Pard.Application.Common.Abstractions.Commands;
using Pard.Application.Common.Interfaces;

namespace Pard.Application.Records.Commands.RestoreRecord
{
    public class RestoreRecordCommandHandler : ICommandHandler<RestoreRecordCommand>
    {
        private readonly IRecordsRepository _repository;

        public RestoreRecordCommandHandler(IRecordsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(RestoreRecordCommand request, CancellationToken cancellationToken)
        {
            await _repository.Restore(request.Id, request.UserId);

            return Unit.Value;
        }
    }
}