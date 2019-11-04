using System;
using Pard.Application.Common.Abstractions.Commands;
using Pard.Application.ViewModels;
using Pard.Domain.Entities.Locations;

namespace Pard.Application.Records.Commands.CreateRecord
{
    public class CreateRecordCommand : ICommand
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