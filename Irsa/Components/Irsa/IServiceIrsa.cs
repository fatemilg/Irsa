
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Components.Irsa
{
    public interface IServiceIrsa
    {
        Task<string> Post(string url, string parameter);
        Task<string> Get(string url);
    }
}