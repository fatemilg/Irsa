using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsa.Models
{
    public class FlightRequest
    {
        public string AdultCount { get; set; }
        public string ChildCount { get; set; }
        public string InfantCount { get; set; }

        public string GoDate { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }

        public string GoDate2 { get; set; }
        public string Origin2 { get; set; }
        public string Destination2 { get; set; }

        public string GoDate3 { get; set; }
        public string Origin3 { get; set; }
        public string Destination3 { get; set; }

        public string BackDate { get; set; }

        public string ConnectionID { get; set; }

        public string Language { get; set; }
        public string Cabin { get; set; }
        public string Currency { get; set; }





        public string FlightType { get; set; }

        public string SecurityGUID { get; set; }

    }
}
