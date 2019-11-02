using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Pard.Application.Common.Abstractions.Commands;
using Pard.Application.Interfaces;

namespace Pard.Application.Records.Commands.DeleteRecord
{
    public class DeleteRecordCommandHandler : ICommandHandler<DeleteRecordCommand>
    {
        private readonly IRecordsRepository _repository;
        
        public DeleteRecordCommandHandler(IRecordsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteRecordCommand request, CancellationToken cancellationToken)
        {
            await _repository.Delete(request.Id, request.UserId);

            return Unit.Value;
        }
    }
}