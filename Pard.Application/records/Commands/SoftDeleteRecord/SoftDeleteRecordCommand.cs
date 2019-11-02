using System;
using Pard.Application.Common.Abstractions.Commands;

namespace Pard.Application.Records.Commands.SoftDeleteRecord
{
    public class SoftDeleteRecordCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
