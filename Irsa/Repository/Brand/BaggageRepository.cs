using Irsa.EFCore;
using Irsa.Repository.Base;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsa.Repository.Brand
{
    public class BaggageRepository : RepositoryBase<Models.Baggage>, IBaggageRepository
    {
        public BaggageRepository(RepositoryContext RepositoryContext)
        : base(RepositoryContext)
        {
        }

        public async  Task<IEnumerable<Models.Baggage>> GetAllBaggage()
        {
            return  await GetAllAsync();
        }
    }
}
