using Pard.Application.Common.Abstractions.Queries;
using Pard.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace Pard.Application.Records.Queries.GetRecordsQuery
{
    public class GetRecordsQuery : IQuery<IEnumerable<RecordViewModel>>
    {
        public Guid UserId { get; set; }
        public bool IsFinished { get; set; }
    }
}
