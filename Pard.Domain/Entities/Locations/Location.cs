using System;
using Pard.Domain.Entities.Records;

namespace Pard.Domain.Entities.Locations
{
    public class Location
    {
        public Guid Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressCountry { get; set; }

        public Guid RecordId { get; set; }
        public Record Record { get; set; }
    }
}