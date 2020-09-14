using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Irsa.XMLModels.Retrieve_System_Fare_Quote_Response
{

    public class NameSpaceList
    {
        public const string s = "http://schemas.xmlsoap.org/soap/envelope/";
        public const string xmlns = "http://tempuri.org/";
        public const string a = "http://schemas.datacontract.org/2004/07/Radixx.ConnectPoint.Pricing.Response";
        public const string i = "http://www.w3.org/2001/XMLSchema-instance/";
        public const string b = "http://schemas.datacontract.org/2004/07/Radixx.ConnectPoint.Exceptions";

    }

    //Retrieve_System_Fare_Quote_Response
    [XmlRoot(ElementName = "Envelope", Namespace = NameSpaceList.s)]
    public class Retrieve_System_Fare_Quote_Response
    {
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Namespaces = new XmlSerializerNamespaces();

        [XmlElement(ElementName = "Body", Namespace = NameSpaceList.s)]
        public Body Body { get; set; }

        public Retrieve_System_Fare_Quote_Response()
        {

            Namespaces.Add("soapenv", NameSpaceList.s);

        }
    }

    //Body
    [XmlRoot(ElementName = "Body", Namespace = NameSpaceList.s)]
    public class Body
    {
        [XmlElement(ElementName = "RetrieveSystemFareQuoteResponse", Namespace = NameSpaceList.xmlns)]
        public RetrieveSystemFareQuoteResponse RetrieveSystemFareQuoteResponse { get; set; }
    }

    //RetrieveSystemFareQuoteResponse
    [XmlRoot(ElementName = "RetrieveSystemFareQuoteResponse", Namespace = NameSpaceList.xmlns)]
    public class RetrieveSystemFareQuoteResponse
    {
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Namespaces = new XmlSerializerNamespaces();

        [XmlElement(ElementName = "RetrieveSystemFareQuoteResult", Namespace = NameSpaceList.xmlns)]
        public RetrieveSystemFareQuoteResult RetrieveSystemFareQuoteResult { get; set; }

        public RetrieveSystemFareQuoteResponse()
        {
            Namespaces.Add("xmlns", NameSpaceList.xmlns);

        }
    }

    //RetrieveSystemFareQuoteResult
    [XmlRoot(ElementName = "RetrieveSystemFareQuoteResult", Namespace = NameSpaceList.xmlns)]
    public class RetrieveSystemFareQuoteResult
    {
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Namespaces = new XmlSerializerNamespaces();


      [XmlElement(ElementName = "SystemFareQuote", Namespace = NameSpaceList.a)]
        public SystemFareQuote SystemFareQuote { get; set; }

        public RetrieveSystemFareQuoteResult()
        {
            Namespaces.Add("a", NameSpaceList.a);
            Namespaces.Add("i", NameSpaceList.i);


        }
    }

    [XmlRoot(ElementName = "SystemFareQuote", Namespace = NameSpaceList.a)]
    public class SystemFareQuote
    {
        [XmlElement(ElementName = "ViewFareQuote", Namespace = NameSpaceList.a)]
        public ViewFareQuote ViewFareQuote { get; set; }
    }

    [XmlRoot(ElementName = "ViewFareQuote", Namespace = NameSpaceList.a)]
    public class ViewFareQuote
    {
        [XmlElement(ElementName = "FlightSegments", Namespace = NameSpaceList.a)]
        public FlightSegments FlightSegments { get; set; }


        [XmlElement(ElementName = "LegDetails", Namespace = NameSpaceList.a)]
        public LegDetails LegDetails { get; set; }

        [XmlElement(ElementName = "SegmentDetails", Namespace = NameSpaceList.a)]
        public SegmentDetails SegmentDetails { get; set; }
    }


    //SegmentDetails
    [XmlRoot(ElementName = "SegmentDetails", Namespace = NameSpaceList.a)]
    public class SegmentDetails
    {

        [XmlElement(ElementName = "SegmentDetail", Namespace = NameSpaceList.a)]
        public List<SegmentDetail> SegmentDetail { get; set; }

    }


    //SegmentDetail
    [XmlRoot(ElementName = "SegmentDetail", Namespace = NameSpaceList.a)]
    public class SegmentDetail
    {

        [XmlElement(ElementName = "LFID", Namespace = NameSpaceList.a)]
        public string LFID { get; set; }

        [XmlElement(ElementName = "Origin", Namespace = NameSpaceList.a)]
        public string Origin { get; set; }

        [XmlElement(ElementName = "Destination", Namespace = NameSpaceList.a)]
        public string Destination { get; set; }

        [XmlElement(ElementName = "DepartureDate", Namespace = NameSpaceList.a)]
        public string DepartureDate { get; set; }

        [XmlElement(ElementName = "CarrierCode", Namespace = NameSpaceList.a)]
        public string CarrierCode { get; set; }

        [XmlElement(ElementName = "ArrivalDate", Namespace = NameSpaceList.a)]
        public string ArrivalDate { get; set; }

        [XmlElement(ElementName = "Stops", Namespace = NameSpaceList.a)]
        public string Stops { get; set; }

        [XmlElement(ElementName = "FlightTime", Namespace = NameSpaceList.a)]
        public string FlightTime { get; set; }

        [XmlElement(ElementName = "AircraftType", Namespace = NameSpaceList.a)]
        public string AircraftType { get; set; }

        [XmlElement(ElementName = "SellingCarrier", Namespace = NameSpaceList.a)]
        public string SellingCarrier { get; set; }

        [XmlElement(ElementName = "FlightNum", Namespace = NameSpaceList.a)]
        public string FlightNum { get; set; }

        [XmlElement(ElementName = "OperatingCarrier", Namespace = NameSpaceList.a)]
        public string OperatingCarrier { get; set; }

        [XmlElement(ElementName = "OperatingFlightNum", Namespace = NameSpaceList.a)]
        public string OperatingFlightNum { get; set; }
        [XmlElement(ElementName = "AircraftDescription", Namespace = NameSpaceList.a)]
        public string AircraftDescription { get; set; }

    }


    //LegDetails
    [XmlRoot(ElementName = "LegDetails", Namespace = NameSpaceList.a)]
    public class LegDetails
    {

        [XmlElement(ElementName = "LegDetail", Namespace = NameSpaceList.a)]
        public List<LegDetail> LegDetail { get; set; }

    }

    //LegDetail
    [XmlRoot(ElementName = "LegDetail", Namespace = NameSpaceList.a)]
    public class LegDetail
    {

        [XmlElement(ElementName = "PFID", Namespace = NameSpaceList.a)]
        public string PFID { get; set; }

        [XmlElement(ElementName = "DepartureDate", Namespace = NameSpaceList.a)]
        public string DepartureDate { get; set; }

        [XmlElement(ElementName = "Origin", Namespace = NameSpaceList.a)]
        public string Origin { get; set; }

        [XmlElement(ElementName = "Destination", Namespace = NameSpaceList.a)]
        public string Destination { get; set; }

        [XmlElement(ElementName = "FlightNum", Namespace = NameSpaceList.a)]
        public string FlightNum { get; set; }

        [XmlElement(ElementName = "International", Namespace = NameSpaceList.a)]
        public string International { get; set; }

        [XmlElement(ElementName = "ArrivalDate", Namespace = NameSpaceList.a)]
        public string ArrivalDate { get; set; }

        [XmlElement(ElementName = "FlightTime", Namespace = NameSpaceList.a)]
        public string FlightTime { get; set; }

        [XmlElement(ElementName = "OperatingCarrier", Namespace = NameSpaceList.a)]
        public string OperatingCarrier { get; set; }

        [XmlElement(ElementName = "FromTerminal", Namespace = NameSpaceList.a)]
        public string FromTerminal { get; set; }

        [XmlElement(ElementName = "ToTerminal", Namespace = NameSpaceList.a)]
        public string ToTerminal { get; set; }

        [XmlElement(ElementName = "AircraftType", Namespace = NameSpaceList.a)]
        public string AircraftType
        {
            get; set;
        }
        [XmlElement(ElementName = "AircraftDescription", Namespace = NameSpaceList.a)]
        public string AircraftDescription { get; set; }

        [XmlElement(ElementName = "DeiDisclosure", Namespace = NameSpaceList.a)]
        public string DeiDisclosure { get; set; }

        [XmlElement(ElementName = "AircraftLayoutName", Namespace = NameSpaceList.a)]
        public string AircraftLayoutName { get; set; }

        [XmlElement(ElementName = "ServiceType", Namespace = NameSpaceList.a)]
        public string ServiceType { get; set; }

    }





    //FlightSegments
    [XmlRoot(ElementName = "FlightSegments", Namespace = NameSpaceList.a)]
    public class FlightSegments
    {

        [XmlElement(ElementName = "FlightSegment", Namespace = NameSpaceList.a)]
        public List<FlightSegment> FlightSegment { get; set; }

    }

    //FlightSegment
    [XmlRoot(ElementName = "FlightSegment", Namespace = NameSpaceList.a)]
    public class FlightSegment
    {

        [XmlElement(ElementName = "LFID", Namespace = NameSpaceList.a)]
        public string LFID { get; set; }

        [XmlElement(ElementName = "DepartureDate", Namespace = NameSpaceList.a)]
        public string DepartureDate { get; set; }

        [XmlElement(ElementName = "ArrivalDate", Namespace = NameSpaceList.a)]
        public string ArrivalDate { get; set; }

        [XmlElement(ElementName = "LegCount", Namespace = NameSpaceList.a)]
        public int LegCount { get; set; }

        [XmlElement(ElementName = "International", Namespace = NameSpaceList.a)]
        public string International { get; set; }

        [XmlElement(ElementName = "FareTypes", Namespace = NameSpaceList.a)]
        public FareTypes FareTypes { get; set; }

        [XmlElement(ElementName = "FlightLegDetails", Namespace = NameSpaceList.a)]
        public FlightLegDetails FlightLegDetails { get; set; }


    }

    //FareTypes
    [XmlRoot(ElementName = "FareTypes", Namespace = NameSpaceList.a)]
    public class FareTypes
    {
        [XmlElement(ElementName = "FareType", Namespace = NameSpaceList.a)]
        public List<FareType> FareType { get; set; }
    }

    //FareType
    [XmlRoot(ElementName = "FareType", Namespace = NameSpaceList.a)]
    public class FareType
    {
        [XmlElement(ElementName = "FareTypeID", Namespace = NameSpaceList.a)]
        public string FareTypeID { get; set; }
        [XmlElement(ElementName = "FareTypeName", Namespace = NameSpaceList.a)]
        public string FareTypeName { get; set; }
        [XmlElement(ElementName = "FilterRemove", Namespace = NameSpaceList.a)]
        public string FilterRemove { get; set; }
        [XmlElement(ElementName = "FareInfos", Namespace = NameSpaceList.a)]
        public FareInfos FareInfos { get; set; }
    }

    //FareInfos
    [XmlRoot(ElementName = "FareInfos", Namespace = NameSpaceList.a)]
    public class FareInfos
    {
        [XmlElement(ElementName = "FareInfo", Namespace = NameSpaceList.a)]
        public List<FareInfo> FareInfo { get; set; }
    }

    //FareInfo
    [XmlRoot(ElementName = "FareInfo", Namespace = NameSpaceList.a)]
    public class FareInfo
    {
        [XmlElement(ElementName = "FareID", Namespace = NameSpaceList.a)]
        public string FareID { get; set; }
        [XmlElement(ElementName = "FCCode", Namespace = NameSpaceList.a)]
        public string FCCode { get; set; }
        [XmlElement(ElementName = "FBCode", Namespace = NameSpaceList.a)]
        public string FBCode { get; set; }
        [XmlElement(ElementName = "BaseFareAmtNoTaxes", Namespace = NameSpaceList.a)]
        public string BaseFareAmtNoTaxes { get; set; }
        [XmlElement(ElementName = "BaseFareAmt", Namespace = NameSpaceList.a)]
        public string BaseFareAmt { get; set; }
        [XmlElement(ElementName = "FareAmtNoTaxes", Namespace = NameSpaceList.a)]
        public string FareAmtNoTaxes { get; set; }
        [XmlElement(ElementName = "FareAmt", Namespace = NameSpaceList.a)]
        public string FareAmt { get; set; }
        [XmlElement(ElementName = "BaseFareAmtInclTax", Namespace = NameSpaceList.a)]
        public string BaseFareAmtInclTax { get; set; }
        [XmlElement(ElementName = "FareAmtInclTax", Namespace = NameSpaceList.a)]
        public string FareAmtInclTax { get; set; }
        [XmlElement(ElementName = "PvtFare", Namespace = NameSpaceList.a)]
        public string PvtFare { get; set; }
        [XmlElement(ElementName = "PTCID", Namespace = NameSpaceList.a)]
        public string PTCID { get; set; }
        [XmlElement(ElementName = "Cabin", Namespace = NameSpaceList.a)]
        public string Cabin { get; set; }
        [XmlElement(ElementName = "SeatsAvailable", Namespace = NameSpaceList.a)]
        public int SeatsAvailable { get; set; }
        [XmlElement(ElementName = "InfantSeatsAvailable", Namespace = NameSpaceList.a)]
        public string InfantSeatsAvailable { get; set; }
        [XmlElement(ElementName = "FareScheduleID", Namespace = NameSpaceList.a)]
        public string FareScheduleID { get; set; }
        [XmlElement(ElementName = "PromotionID", Namespace = NameSpaceList.a)]
        public string PromotionID { get; set; }
        [XmlElement(ElementName = "RoundTrip", Namespace = NameSpaceList.a)]
        public string RoundTrip { get; set; }
        [XmlElement(ElementName = "DisplayFareAmt", Namespace = NameSpaceList.a)]
        public string DisplayFareAmt { get; set; }
        [XmlElement(ElementName = "DisplayTaxSum", Namespace = NameSpaceList.a)]
        public string DisplayTaxSum { get; set; }
        [XmlElement(ElementName = "SpecialMarketed", Namespace = NameSpaceList.a)]
        public string SpecialMarketed { get; set; }
        [XmlElement(ElementName = "WaitList", Namespace = NameSpaceList.a)]
        public string WaitList { get; set; }
        [XmlElement(ElementName = "SpaceAvailable", Namespace = NameSpaceList.a)]
        public string SpaceAvailable { get; set; }
        [XmlElement(ElementName = "PositiveSpace", Namespace = NameSpaceList.a)]
        public string PositiveSpace { get; set; }
        [XmlElement(ElementName = "PromotionCatID", Namespace = NameSpaceList.a)]
        public string PromotionCatID { get; set; }
        [XmlElement(ElementName = "CommissionAmount", Namespace = NameSpaceList.a)]
        public string CommissionAmount { get; set; }
        [XmlElement(ElementName = "PromotionAmount", Namespace = NameSpaceList.a)]
        public string PromotionAmount { get; set; }

    }


    //FlightLegDetails
    [XmlRoot(ElementName = "FlightLegDetails", Namespace = NameSpaceList.a)]
    public class FlightLegDetails
    {
        [XmlElement(ElementName = "FlightLegDetail", Namespace = NameSpaceList.a)]
        public List<FlightLegDetail> FlightLegDetail { get; set; }
    }


    //FlightLegDetail
    [XmlRoot(ElementName = "FlightLegDetail", Namespace = NameSpaceList.a)]
    public class FlightLegDetail
    {
        [XmlElement(ElementName = "PFID", Namespace = NameSpaceList.a)]
        public string PFID { get; set; }
        [XmlElement(ElementName = "DepartureDate", Namespace = NameSpaceList.a)]
        public string DepartureDate { get; set; }

    }

}
