using Irsa.Configs;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Threading.Tasks;

namespace Components.Irsa
{

    public class ServiceIrsa : IServiceIrsa
    {

        public ServiceIrsa() { }


        public async Task<string> Post(string url, string parameter)
        {

            var req = new RestRequest(Method.POST);
            req.AddHeader("ApplicationVersion", "1.9.3-GP");
            req.AddHeader("Mobile-Agent", "MobileApp/Android/v-45/c07669c313e08e02");
            req.AddHeader("cache-control", "no-cache");
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(parameter);

            var client = new RestClient(url);
            var response = await client.ExecuteAsync(req);
            //var obj = JObject.Parse(response.Content);
            return response.Content;
        }
        public async Task<string> Get(string url)
        {
            var req = new RestRequest(Method.GET);
            req.AddHeader("ApplicationVersion", "1.9.3-GP");
            req.AddHeader("Mobile-Agent", "MobileApp/Android/v-45/c07669c313e08e02");
            req.AddHeader("cache-control", "no-cache");
            req.AddHeader("Content-Type", "application/json");

            var client = new RestClient(url);
            var response = await client.ExecuteAsync(req);
            //var obj = JObject.Parse(response.Content);
            return response.Content;
        }
    }
}