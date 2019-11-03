using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Pard.Application.Common.Abstractions.Queries;
using Pard.Application.Common.Interfaces;
using Pard.Application.ViewModels;
using Pard.Domain.Entities.Records;

namespace Pard.Application.Records.Queries.GetArchivedRecordsQuery
{
    public class GetArchivedRecordsQueryHandler : IQueryHandler<GetArchivedRecordsQuery, IEnumerable<RecordViewModel>>
    {
        private readonly IRecordsRepository _repository;
        private readonly IMapper _mapper;

        public GetArchivedRecordsQueryHandler(IRecordsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RecordViewModel>> Handle(GetArchivedRecordsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetArchivedRecordsForUser(request.UserId);
            var result = _mapper.Map<IEnumerable<Record>, IEnumerable<RecordViewModel>>(entities);

            return result;
        }
    }
}