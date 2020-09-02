using Components.Irsa;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Irsa.Components.Soap
{
    public class XmlService : IXmlService
    {


        public async Task<string> InvokeService(string Url, string Param,string SoapAction)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    var subparam = Param.Replace(Param.Substring(0, 39), "");

                    var Content = new StringContent(subparam, Encoding.UTF8, "text/xml");
                    client.DefaultRequestHeaders.Add("SOAPAction", SoapAction);

                    using (var response = await client.PostAsync(Url, Content))
                    {
                        var soapResponse = await response.Content.ReadAsStringAsync();
                        return soapResponse;
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }


        }





    }
}
