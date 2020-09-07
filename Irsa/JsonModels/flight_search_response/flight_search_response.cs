using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsa.JsonModels.flight_search_response
{
    public class flight_search_response
    {
        public Guid SearchID { get; set; }
        public string SearchTimeSeconds { get; set; }
        public string CurrencyCode { get; set; }
        public string SearchType { get; set; }
        public bool DomesticFlight { get; set; }
        public bool HasMoreResult { get; set; }
        public string AllFlightsCount { get; set; }
        public FlightItems FlightItems { get; set; }


    }
    public class FlightItems
    {
        public List<Flights> Flights { get; set; }
    }

    public class Flights
    {
        public Guid FlightID { get; set; }
        public string PricingType { get; set; }
        public string PackageID { get; set; }
        public string ProviderCode { get; set; }
        public int ConnectionID { get; set; }
        public string SystemFlight { get; set; }
        public string CanBook { get; set; }
        public string CanReserve { get; set; }
        public string CabinClass { get; set; }
        public string Stops { get; set; }
        public string TotalFlightDuration { get; set; }
        public int TotalFareAmout { get; set; }
        public List<Fares> Fares { get; set; }
        public List<Legs> Legs { get; set; }



    }

    public class Fares
    {
        public string PassengerType { get; set; }
        public int Quantity { get; set; }
        public int BaseFare { get; set; }
        public int Tax { get; set; }
        public string ServiceCommission { get; set; }
        public string ServiceCommissionOnFare { get; set; }
        public string ServiceProviderCost { get; set; }
        public string ServiceFee { get; set; }
        public string APICost { get; set; }
        public string Commission { get; set; }
        public string Supplement { get; set; }
        public string Fuel { get; set; }
        public string Discount { get; set; }

        public int Total { get; set; }
    }

    public class Legs
    {
        public string FlightNumber { get; set; }
        public string AircraftCode { get; set; }
        public string MarketingAirline { get; set; }
        public string OperatingAirline { get; set; }
        public string FareClass { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public string FlightDurationMinutes { get; set; }
        public string LayoverDurationMinutes { get; set; }
        public string SeatCount { get; set; }
        public List<BaggageItems> BaggageItems { get; set; }
    }

    public class BaggageItems
    {
        public int BaggageDetailID { get; set; }
        public string PassengerType { get; set; }
    }
}
