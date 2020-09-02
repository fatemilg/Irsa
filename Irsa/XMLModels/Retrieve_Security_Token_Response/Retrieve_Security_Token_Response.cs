using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Irsa.XMLModels.Retrieve_Security_Token_Response
{
    public class NameSpaceList
    {
        public const string s = "http://schemas.xmlsoap.org/soap/envelope/";
        public const string a = "http://schemas.datacontract.org/2004/07/Radixx.ConnectPoint.Security.Response";
        public const string b = "http://schemas.datacontract.org/2004/07/Radixx.ConnectPoint.Exceptions";
        public const string i = "http://www.w3.org/2001/XMLSchema-instance";
        public const string xmlns = "http://tempuri.org/";
    }

    //Envelope
    [XmlRoot(ElementName = "Envelope", Namespace = NameSpaceList.s)]
    public class Retrieve_Security_Token_Response
    {
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Namespaces = new XmlSerializerNamespaces();

        [XmlElement(ElementName = "Body", Namespace = NameSpaceList.s)]
        public Body Body { get; set; }


        public Retrieve_Security_Token_Response()
        {

            Namespaces.Add("s", NameSpaceList.s);
        }

    }



    //Body
    [XmlRoot(ElementName = "Body", Namespace = NameSpaceList.s)]
    public class Body
    {
        [XmlElement(ElementName = "RetrieveSecurityTokenResponse", Namespace = NameSpaceList.xmlns)]
        public RetrieveSecurityTokenResponse RetrieveSecurityTokenResponse { get; set; }
    }


    //RetrieveSecurityTokenResponse
    [XmlRoot(ElementName = "RetrieveSecurityTokenResponse", Namespace = NameSpaceList.xmlns)]
    public class RetrieveSecurityTokenResponse
    {
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Namespaces = new XmlSerializerNamespaces();

        [XmlElement(ElementName = "RetrieveSecurityTokenResult", Namespace = NameSpaceList.xmlns)]
        public RetrieveSecurityTokenResult RetrieveSecurityTokenResult { get; set; }

        public RetrieveSecurityTokenResponse()
        {
            Namespaces.Add("xmlns", NameSpaceList.xmlns);

        }
    }

    //RetrieveSecurityTokenResult
    [XmlRoot(ElementName = "RetrieveSecurityTokenResult", Namespace = NameSpaceList.xmlns)]
    public class RetrieveSecurityTokenResult
    {
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Namespaces = new XmlSerializerNamespaces();

        [XmlElement(ElementName = "Exceptions", Namespace = NameSpaceList.a)]
        public Exceptions Exceptions { get; set; }


        [XmlElement(ElementName = "SecurityToken", Namespace = NameSpaceList.a)]
        public string SecurityToken { get; set; }

        public RetrieveSecurityTokenResult()
        {
            Namespaces.Add("a", NameSpaceList.a);
            Namespaces.Add("i", NameSpaceList.i);


        }
    }

    //Exceptions
    [XmlRoot(ElementName = "Exceptions", Namespace = NameSpaceList.a)]
    public class Exceptions
    {

        [XmlElement(ElementName = "ExceptionInformation.Exception", Namespace = NameSpaceList.b)]
        public ExceptionInformationException ExceptionInformationException { get; set; }

    }


    //ExceptionInformation.Exception
    [XmlRoot(ElementName = "ExceptionInformation.Exception", Namespace = NameSpaceList.b)]
    public class ExceptionInformationException
    {
        [XmlElement(ElementName = "ExceptionCode", Namespace = NameSpaceList.b)]
        public string ExceptionCode { get; set; }


        [XmlElement(ElementName = "ExceptionDescription", Namespace = NameSpaceList.b)]
        public string ExceptionDescription { get; set; }

        [XmlElement(ElementName = "ExceptionSource", Namespace = NameSpaceList.b)]
        public string ExceptionSource { get; set; }

        [XmlElement(ElementName = "ExceptionLevel", Namespace = NameSpaceList.b)]
        public string ExceptionLevel { get; set; }
    }

}
