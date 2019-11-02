using System;
using Pard.Application.Common.Abstractions.Commands;

namespace Pard.Application.Records.Commands.DeleteRecord
{
    public class DeleteRecordCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
