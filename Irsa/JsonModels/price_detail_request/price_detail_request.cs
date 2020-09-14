using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsa.JsonModels.price_detail_request
{
    public class price_detail_request
    {
        public string SearchID { get; set; }

        public string[] FlightIDs { get; set; }
        public TravelerAvail TravelerAvail { get; set; }
        public List<AirItinerary> AirItineraries { get; set; }
        public ExtraServices ExtraServices { get; set; }

        public string Currency { get; set; }
        public string LanguageCode { get; set; }
        public string FareFamilyID { get; set; }
        public string SecurityGUID { get; set; }
    }

    public class TravelerAvail
    {
        public string AdultCount { get; set; }
        public string ChildCount { get; set; }
        public string InfantCount { get; set; }

    }
    public class ExtraServices
    {
        public bool GetAncillaryList { get; set; }
        public bool GetSeatMap { get; set; }
        public bool GetFareFamily { get; set; }

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


}
