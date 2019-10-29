using System;

namespace Pard.Application.ViewModels
{
    public class LocationViewModel
    {
        public string Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public object Viewport { get; set; }
        public double Zoom { get; set; } = 5;
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressCountry { get; set; }
        public string RecordId { get; set; }
        public MarkerViewModel Marker { get; set; }
    }
}