using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsa.Models
{
    public class Airport
    {
        public string Priority { get; set; }
        public string AirportCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TimeZoneTimeSpan { get; set; }
        public string AirportName { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string CityCode { get; set; }
        public string CountryCode { get; set; }

    }
}
