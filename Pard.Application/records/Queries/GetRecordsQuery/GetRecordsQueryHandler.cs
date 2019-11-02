using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Pard.Application.Common.Abstractions.Queries;
using Pard.Application.Interfaces;
using Pard.Application.ViewModels;
using Pard.Domain.Entities.Records;

namespace Pard.Application.Records.Queries.GetRecordsQuery
{
    public class GetRecordsQueryHandler : IQueryHandler<GetRecordsQuery, IEnumerable<RecordViewModel>>
    {
        private readonly IRecordsRepository _repository;
        private readonly IMapper _mapper;

        public GetRecordsQueryHandler(IRecordsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RecordViewModel>> Handle(GetRecordsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetRecords(request.IsFinished, request.UserId);
            var result = _mapper.Map<IEnumerable<Record>, IEnumerable<RecordViewModel>>(entities);

            return result;
        }
    }
}