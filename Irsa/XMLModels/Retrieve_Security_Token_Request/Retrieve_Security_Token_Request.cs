using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Irsa.XMLModels.Retrieve_Security_Token_Request
{
    public class NameSpaceList
    {
        public const string soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
        public const string rad = "http://schemas.datacontract.org/2004/07/Radixx.ConnectPoint.Request";
        public const string radl = "http://schemas.datacontract.org/2004/07/Radixx.ConnectPoint.Security.Request";
        public const string tem = "http://tempuri.org/";


    }

    [XmlRoot(ElementName = "Envelope", Namespace = NameSpaceList.soapenv)]
    public class Retrieve_Security_Token_Request
    {
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Namespaces = new XmlSerializerNamespaces();
    
        [XmlElement(ElementName = "Header", Namespace = NameSpaceList.soapenv)]
        public Header Header { get; set; }
        [XmlElement(ElementName = "Body", Namespace = NameSpaceList.soapenv)]
        public Body Body { get; set; }


        public Retrieve_Security_Token_Request()
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
        [XmlElement(ElementName = "RetrieveSecurityToken", Namespace = NameSpaceList.tem)]
        public RetrieveSecurityToken RetrieveSecurityToken { get; set; }
    }


    [XmlRoot(ElementName = "RetrieveSecurityToken", Namespace = NameSpaceList.tem)]
    public class RetrieveSecurityToken
    {
        [XmlElement(ElementName = "RetrieveSecurityTokenRequest", Namespace = NameSpaceList.tem)]
        public RetrieveSecurityTokenRequest RetrieveSecurityTokenRequest { get; set; }
    }


    [XmlRoot(ElementName = "RetrieveSecurityTokenRequest", Namespace = NameSpaceList.tem)]
    public class RetrieveSecurityTokenRequest
    {
        [XmlElement(ElementName = "CarrierCodes", Namespace = NameSpaceList.rad)]
        public CarrierCodes CarrierCodes { get; set; }


        [XmlElement(ElementName = "LogonID", Namespace = NameSpaceList.radl)]
        public string LogonID { get; set; }

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
