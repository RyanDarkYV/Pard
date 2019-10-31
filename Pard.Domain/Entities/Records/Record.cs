using System;
using Pard.Domain.Entities.Locations;

namespace Pard.Domain.Entities.Records
{
    public class Record
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
        
        public Location Location { get; set; }
    }
}
