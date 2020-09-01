using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsa.Models
{
    public class Flights
    {
        public string FlightID { get; set; }
        public string PricingType { get; set; }
        public string PackageID { get; set; }
        public string ProviderCode { get; set; }
        public string ConnectionID { get; set; }
        public string TotalFareAmout { get; set; }
    }
}
