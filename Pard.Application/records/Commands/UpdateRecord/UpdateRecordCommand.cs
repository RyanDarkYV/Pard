using Pard.Application.Common.Abstractions.Commands;
using Pard.Application.ViewModels;
using System;

namespace Pard.Application.Records.Commands.UpdateRecord
{
    public class UpdateRecordCommand : ICommand
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public LocationViewModel Location { get; set; }
        public string UserId { get; set; }
    }
}
