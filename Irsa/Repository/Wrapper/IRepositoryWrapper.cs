using Irsa.Repository.Brand;
using System.Threading.Tasks;

namespace Irsa.Repository.Wrapper
{
    public interface IRepositoryWrapper
    {

        IBaggageRepository Baggage { get; }


        Task SaveAsync();
    }
}
