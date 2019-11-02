using System;
using Pard.Application.Common.Abstractions.Commands;

namespace Pard.Application.Records.Commands.RestoreRecord
{
    public class RestoreRecordCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
