using System;
using Pard.Application.Common.Abstractions.Queries;
using Pard.Application.ViewModels;
using System.Collections.Generic;

namespace Pard.Application.Records.Queries.GetArchivedRecordsQuery
{
    public class GetArchivedRecordsQuery : IQuery<IEnumerable<RecordViewModel>>
    {
        public Guid UserId { get; set; }
    }
}
