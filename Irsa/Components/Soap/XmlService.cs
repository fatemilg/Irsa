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
using System.Xml.Linq;

namespace Irsa.Components.Soap
{
    public class XmlService : IXmlService
    {


        public async Task<string> PostXml(string Url, string param)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    var subparam = param.Replace(param.Substring(0, 39), "");

                    var Content = new StringContent(subparam, Encoding.UTF8, "text/xml");

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





//using (var client = new HttpClient())
//{
//    var subparam = param.Replace(param.Substring(0, 39), "");
//    var request = new HttpRequestMessage()
//    {
//        RequestUri = new Uri(Url),
//        Method = HttpMethod.Post
//    };


//    var Content = new StringContent(subparam, Encoding.UTF8, "text/xml");

//    request.Content = new StringContent(subparam, Encoding.UTF8, "text/xml");

//    request.Headers.Clear();
//    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
//    request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
//    request.Headers.Add("SOAPAction", "");

//    HttpResponseMessage response = client.PostAsync(Url, Content).Result;

//    if (!response.IsSuccessStatusCode)
//    {
//        throw new Exception();
//    }

//    Task<Stream> streamTask = response.Content.ReadAsStreamAsync();
//    Stream stream = streamTask.Result;
//    var sr = new StreamReader(stream);
//    var soapResponse = XDocument.Load(sr);
//    return soapResponse.ToString();
//}