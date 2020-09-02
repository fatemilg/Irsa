using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Irsa.XMLModels.Login_Travel_Agent_Request
{

    public class NameSpaceList
    {
        public const string soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
        public const string rad = "http://schemas.datacontract.org/2004/07/Radixx.ConnectPoint.Request";
        public const string radl = "http://schemas.datacontract.org/2004/07/Radixx.ConnectPoint.TravelAgents.Request";
        public const string tem = "http://tempuri.org/";


    }

    [XmlRoot(ElementName = "Envelope", Namespace = NameSpaceList.soapenv)]
    public class Login_Travel_Agent_Request
    {
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Namespaces = new XmlSerializerNamespaces();

        [XmlElement(ElementName = "Header", Namespace = NameSpaceList.soapenv)]
        public Header Header { get; set; }
        [XmlElement(ElementName = "Body", Namespace = NameSpaceList.soapenv)]
        public Body Body { get; set; }


        public Login_Travel_Agent_Request()
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
        [XmlElement(ElementName = "LoginTravelAgent", Namespace = NameSpaceList.tem)]
        public LoginTravelAgent LoginTravelAgent { get; set; }
    }


    [XmlRoot(ElementName = "LoginTravelAgent", Namespace = NameSpaceList.tem)]
    public class LoginTravelAgent
    {
        [XmlElement(ElementName = "LoginTravelAgentRequest", Namespace = NameSpaceList.tem)]
        public LoginTravelAgentRequest LoginTravelAgentRequest { get; set; }
    }


    [XmlRoot(ElementName = "LoginTravelAgentRequest", Namespace = NameSpaceList.tem)]
    public class LoginTravelAgentRequest
    {
        [XmlElement(ElementName = "SecurityGUID", Namespace = NameSpaceList.rad)]
        public string SecurityGUID { get; set; }

        [XmlElement(ElementName = "CarrierCodes", Namespace = NameSpaceList.rad)]
        public CarrierCodes CarrierCodes { get; set; }

        [XmlElement(ElementName = "ClientIPAddress", Namespace = NameSpaceList.rad)]
        public string ClientIPAddress { get; set; }

        [XmlElement(ElementName = "HistoricUserName", Namespace = NameSpaceList.rad)]
        public string HistoricUserName { get; set; }


        [XmlElement(ElementName = "IATANumber", Namespace = NameSpaceList.radl)]
        public string IATANumber { get; set; }

        [XmlElement(ElementName = "UserName", Namespace = NameSpaceList.radl)]
        public string UserName { get; set; }

        [XmlElement(ElementName = "Password", Namespace = NameSpaceList.radl)]
        public string Password { get; set; }

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
