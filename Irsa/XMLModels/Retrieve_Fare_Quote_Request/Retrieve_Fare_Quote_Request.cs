using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Irsa.XMLModels.Retrieve_Fare_Quote_Request
{

    public class NameSpaceList
    {
        public const string soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
        public const string rad = "http://schemas.datacontract.org/2004/07/Radixx.ConnectPoint.Request";
        public const string radl = "http://schemas.datacontract.org/2004/07/Radixx.ConnectPoint.Pricing.Request.FareQuote";
        public const string tem = "http://tempuri.org/";
        public const string arr = "http://schemas.microsoft.com/2003/10/Serialization/Arrays";

    }

    [XmlRoot(ElementName = "Envelope", Namespace = NameSpaceList.soapenv)]
    public class Retrieve_Fare_Quote_Request
    {
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Namespaces = new XmlSerializerNamespaces();

        [XmlElement(ElementName = "Header", Namespace = NameSpaceList.soapenv)]
        public Header Header { get; set; }
        [XmlElement(ElementName = "Body", Namespace = NameSpaceList.soapenv)]
        public Body Body { get; set; }


        public Retrieve_Fare_Quote_Request()
        {

            Namespaces.Add("soapenv", NameSpaceList.soapenv);
            Namespaces.Add("rad", NameSpaceList.rad);
            Namespaces.Add("radl", NameSpaceList.radl);
            Namespaces.Add("tem", NameSpaceList.tem);
            Namespaces.Add("arr", NameSpaceList.arr);

        }

    }


    [XmlRoot(ElementName = "Header", Namespace = NameSpaceList.soapenv)]
    public class Header
    {

    }

    [XmlRoot(ElementName = "Body", Namespace = NameSpaceList.soapenv)]
    public class Body
    {
        [XmlElement(ElementName = "RetrieveFareQuote", Namespace = NameSpaceList.tem)]
        public RetrieveFareQuote RetrieveFareQuote { get; set; }
    }


    [XmlRoot(ElementName = "RetrieveFareQuote", Namespace = NameSpaceList.tem)]
    public class RetrieveFareQuote
    {
        [XmlElement(ElementName = "RetrieveFareQuoteRequest", Namespace = NameSpaceList.tem)]
        public RetrieveFareQuoteRequest RetrieveFareQuoteRequest { get; set; }
    }


    [XmlRoot(ElementName = "RetrieveFareQuoteRequest", Namespace = NameSpaceList.tem)]
    public class RetrieveFareQuoteRequest
    {
        [XmlElement(ElementName = "SecurityGUID", Namespace = NameSpaceList.rad)]
        public string SecurityGUID { get; set; }

        [XmlElement(ElementName = "CarrierCodes", Namespace = NameSpaceList.rad)]
        public CarrierCodes CarrierCodes { get; set; }

        [XmlElement(ElementName = "ClientIPAddress", Namespace = NameSpaceList.rad)]
        public string ClientIPAddress { get; set; }

        [XmlElement(ElementName = "HistoricUserName", Namespace = NameSpaceList.rad)]
        public string HistoricUserName { get; set; }


        [XmlElement(ElementName = "CurrencyOfFareQuote", Namespace = NameSpaceList.radl)]
        public string CurrencyOfFareQuote { get; set; }

        [XmlElement(ElementName = "PromotionalCode", Namespace = NameSpaceList.radl)]
        public string PromotionalCode { get; set; }

        [XmlElement(ElementName = "IataNumberOfRequestor", Namespace = NameSpaceList.radl)]
        public string IataNumberOfRequestor { get; set; }

        [XmlElement(ElementName = "CorporationID", Namespace = NameSpaceList.radl)]
        public string CorporationID { get; set; }

        [XmlElement(ElementName = "FareFilterMethod", Namespace = NameSpaceList.radl)]
        public string FareFilterMethod { get; set; }

        [XmlElement(ElementName = "FareGroupMethod", Namespace = NameSpaceList.radl)]
        public string FareGroupMethod { get; set; }

        [XmlElement(ElementName = "InventoryFilterMethod", Namespace = NameSpaceList.radl)]
        public string InventoryFilterMethod { get; set; }

        [XmlElement(ElementName = "FareQuoteDetails", Namespace = NameSpaceList.radl)]
        public FareQuoteDetails FareQuoteDetails { get; set; }

        [XmlElement(ElementName = "ProfileId", Namespace = NameSpaceList.radl)]
        public string ProfileId { get; set; }

    }


    [XmlRoot(ElementName = "CarrierCodes", Namespace = NameSpaceList.rad)]
    public class CarrierCodes
    {
        [XmlElement(ElementName = "CarrierCode", Namespace = NameSpaceList.rad)]
        public CarrierCode CarrierCode { get; set; }

    }


    [XmlRoot(ElementName = "CarrierCode", Namespace = NameSpaceList.rad)]
    public class CarrierCode
    {
        [XmlElement(ElementName = "AccessibleCarrierCode", Namespace = NameSpaceList.rad)]
        public string AccessibleCarrierCode { get; set; }

    }


    [XmlRoot(ElementName = "FareQuoteDetails", Namespace = NameSpaceList.radl)]
    public class FareQuoteDetails
    {
        [XmlElement(ElementName = "FareQuoteDetail", Namespace = NameSpaceList.radl)]
        public List<FareQuoteDetail> FareQuoteDetail { get; set; }

    }


    [XmlRoot(ElementName = "FareQuoteDetail", Namespace = NameSpaceList.radl)]
    public class FareQuoteDetail
    {
        [XmlElement(ElementName = "Origin", Namespace = NameSpaceList.radl)]
        public string Origin { get; set; }

        [XmlElement(ElementName = "Destination", Namespace = NameSpaceList.radl)]
        public string Destination { get; set; }

        [XmlElement(ElementName = "UseAirportsNotMetroGroups", Namespace = NameSpaceList.radl)]
        public bool UseAirportsNotMetroGroups { get; set; }

        [XmlElement(ElementName = "UseAirportsNotMetroGroupsAsRule", Namespace = NameSpaceList.radl)]
        public bool UseAirportsNotMetroGroupsAsRule { get; set; }

        [XmlElement(ElementName = "UseAirportsNotMetroGroupsForFrom", Namespace = NameSpaceList.radl)]
        public bool UseAirportsNotMetroGroupsForFrom { get; set; }

        [XmlElement(ElementName = "UseAirportsNotMetroGroupsForTo", Namespace = NameSpaceList.radl)]
        public bool UseAirportsNotMetroGroupsForTo { get; set; }

        [XmlElement(ElementName = "DateOfDeparture", Namespace = NameSpaceList.radl)]
        public string DateOfDeparture { get; set; }

        [XmlElement(ElementName = "FareTypeCategory", Namespace = NameSpaceList.radl)]
        public string FareTypeCategory { get; set; }

        [XmlElement(ElementName = "FareClass", Namespace = NameSpaceList.radl)]
        public string FareClass { get; set; }

        [XmlElement(ElementName = "FareBasisCode", Namespace = NameSpaceList.radl)]
        public string FareBasisCode { get; set; }

        [XmlElement(ElementName = "Cabin", Namespace = NameSpaceList.radl)]
        public string Cabin { get; set; }

        [XmlElement(ElementName = "LFID", Namespace = NameSpaceList.radl)]
        public string LFID { get; set; }

        [XmlElement(ElementName = "OperatingCarrierCode", Namespace = NameSpaceList.radl)]
        public string OperatingCarrierCode { get; set; }

        [XmlElement(ElementName = "MarketingCarrierCode", Namespace = NameSpaceList.radl)]
        public string MarketingCarrierCode { get; set; }

        [XmlElement(ElementName = "NumberOfDaysBefore", Namespace = NameSpaceList.radl)]
        public string NumberOfDaysBefore { get; set; }

        [XmlElement(ElementName = "NumberOfDaysAfter", Namespace = NameSpaceList.radl)]
        public string NumberOfDaysAfter { get; set; }

        [XmlElement(ElementName = "LanguageCode", Namespace = NameSpaceList.radl)]
        public string LanguageCode { get; set; }

        [XmlElement(ElementName = "TicketPackageID", Namespace = NameSpaceList.radl)]
        public string TicketPackageID { get; set; }

        [XmlElement(ElementName = "FareQuoteRequestInfos", Namespace = NameSpaceList.radl)]
        public FareQuoteRequestInfos FareQuoteRequestInfos { get; set; }

        [XmlElement(ElementName = "FareTypeCategories", Namespace = NameSpaceList.radl)]
        public FareTypeCategories FareTypeCategories { get; set; }


    }

    [XmlRoot(ElementName = "FareQuoteRequestInfos", Namespace = NameSpaceList.radl)]
    public class FareQuoteRequestInfos
    {
        [XmlElement(ElementName = "FareQuoteRequestInfo", Namespace = NameSpaceList.radl)]
        public List<FareQuoteRequestInfo> FareQuoteRequestInfo { get; set; }
    }

    [XmlRoot(ElementName = "FareQuoteRequestInfo", Namespace = NameSpaceList.radl)]
    public class FareQuoteRequestInfo
    {
        [XmlElement(ElementName = "PassengerTypeID", Namespace = NameSpaceList.radl)]
        public string PassengerTypeID { get; set; }

        [XmlElement(ElementName = "TotalSeatsRequired", Namespace = NameSpaceList.radl)]
        public string TotalSeatsRequired { get; set; }
    }


    [XmlRoot(ElementName = "FareTypeCategories", Namespace = NameSpaceList.arr)]
    public class FareTypeCategories
    {
        [XmlElement(ElementName = "int", Namespace = NameSpaceList.arr)]
        public string Int { get; set; }
    }


}



