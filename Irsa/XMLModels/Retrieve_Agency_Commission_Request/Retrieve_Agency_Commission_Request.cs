using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Irsa.XMLModels.Retrieve_Agency_Commission_Request
{

    public class NameSpaceList
    {
        public const string soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
        public const string rad = "http://schemas.datacontract.org/2004/07/Radixx.ConnectPoint.Request";
        public const string radl = "http://schemas.datacontract.org/2004/07/Radixx.ConnectPoint.TravelAgents.Request";
        public const string tem = "http://tempuri.org/";

    }

    [XmlRoot(ElementName = "Envelope", Namespace = NameSpaceList.soapenv)]
    public class Retrieve_Agency_Commission_Request
    {
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Namespaces = new XmlSerializerNamespaces();

        [XmlElement(ElementName = "Header", Namespace = NameSpaceList.soapenv)]
        public Header Header { get; set; }
        [XmlElement(ElementName = "Body", Namespace = NameSpaceList.soapenv)]
        public Body Body { get; set; }


        public Retrieve_Agency_Commission_Request()
        {

            Namespaces.Add("soapenv", NameSpaceList.soapenv);
            Namespaces.Add("rad", NameSpaceList.rad);
            Namespaces.Add("radl", NameSpaceList.radl);
            Namespaces.Add("tem", NameSpaceList.tem);
        }

    }


    [XmlRoot(ElementName = "Header", Namespace = NameSpaceList.soapenv)]
    public class Header
    {

    }

    [XmlRoot(ElementName = "Body", Namespace = NameSpaceList.soapenv)]
    public class Body
    {
        [XmlElement(ElementName = "RetrieveAgencyCommission", Namespace = NameSpaceList.tem)]
        public RetrieveAgencyCommission RetrieveAgencyCommission { get; set; }
    }


    [XmlRoot(ElementName = "RetrieveAgencyCommission", Namespace = NameSpaceList.tem)]
    public class RetrieveAgencyCommission
    {
        [XmlElement(ElementName = "RetrieveAgencyCommissionRequest", Namespace = NameSpaceList.tem)]
        public RetrieveAgencyCommissionRequest RetrieveAgencyCommissionRequest { get; set; }
    }


    [XmlRoot(ElementName = "RetrieveAgencyCommissionRequest", Namespace = NameSpaceList.tem)]
    public class RetrieveAgencyCommissionRequest
    {
        [XmlElement(ElementName = "SecurityGUID", Namespace = NameSpaceList.rad)]
        public string SecurityGUID { get; set; }

        [XmlElement(ElementName = "CarrierCodes", Namespace = NameSpaceList.rad)]
        public CarrierCodes CarrierCodes { get; set; }

        [XmlElement(ElementName = "ClientIPAddress", Namespace = NameSpaceList.rad)]
        public string ClientIPAddress { get; set; }

        [XmlElement(ElementName = "HistoricUserName", Namespace = NameSpaceList.rad)]
        public string HistoricUserName { get; set; }


        [XmlElement(ElementName = "CurrencyCode", Namespace = NameSpaceList.radl)]
        public string CurrencyCode { get; set; }


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


}

