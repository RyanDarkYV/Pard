using System;
using FluentValidation.Attributes;
using Pard.Application.ViewModels.Validations;

namespace Pard.Application.ViewModels
{
    [Validator(typeof(RecordViewModelValidator))]
    public class RecordViewModel
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