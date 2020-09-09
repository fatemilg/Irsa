using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsa.JsonModels.flight_search_request
{

    public class flight_saerch_request
    {

        public TravelerAvail TravelerAvail { get; set; }
        public List<AirItinerary> AirItineraries { get; set; }
        public Guid LoginID { get; set; }
        public SearchFilters SearchFilters { get; set; }

        public string FlightType { get; set; }

        public string SecurityGUID { get; set; }
    }

    public class TravelerAvail
    {
        public string AdultCount { get; set; }
        public string ChildCount { get; set; }
        public string InfantCount { get; set; }

    }

    public class AirItinerary
    {

        public string DepartureDate { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string ConnectionID { get; set; }
        public string AllAirportsDestination { get; set; }
        public string AllAirportsOrigin { get; set; }

    }

    public class SearchFilters
    {
        public string DirectFlightsOnly { get; set; }
        public string Currency { get; set; }
        public string MaxPrice { get; set; }
        public string SpecificAirlines { get; set; }
        public string ExcludeAirlines { get; set; }
        public string SpecificAircrafts { get; set; }
        public string ExcludeAircrafts { get; set; }
        public string MandatoryCabin { get; set; }
        public string MaximumItems { get; set; }
        public string SortItem { get; set; }
        public string RemoveDuplicates { get; set; }
        public string ShowFares { get; set; }
        public string Language { get; set; }



    }

}
