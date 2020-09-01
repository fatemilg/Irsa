using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsa.Repository.Brand
{
    public interface IBaggageRepository
    {
        Task<IEnumerable<Models.Baggage>> GetAllBaggage();
    }
}
