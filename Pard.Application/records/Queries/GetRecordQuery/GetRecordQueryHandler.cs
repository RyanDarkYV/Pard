using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Pard.Application.Common.Abstractions.Queries;
using Pard.Application.Common.Interfaces;
using Pard.Application.ViewModels;
using Pard.Domain.Entities.Records;

namespace Pard.Application.Records.Queries.GetRecordQuery
{
    public class GetRecordQueryHandler : IQueryHandler<GetRecordQuery, RecordViewModel>
    {
        private readonly IRecordsRepository _repository;
        private readonly IMapper _mapper;

        public GetRecordQueryHandler(IMapper mapper, IRecordsRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<RecordViewModel> Handle(GetRecordQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetRecordById(request.Id, request.UserId);
            var result = _mapper.Map<Record, RecordViewModel>(entity);

            return result;
        }
    }
}