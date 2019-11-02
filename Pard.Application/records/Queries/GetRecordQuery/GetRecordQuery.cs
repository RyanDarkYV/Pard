using System;
using Pard.Application.Common.Abstractions.Queries;
using Pard.Application.ViewModels;

namespace Pard.Application.Records.Queries.GetRecordQuery
{
    public class GetRecordQuery : IQuery<RecordViewModel>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
