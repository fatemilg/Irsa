using Irsa.EFCore;
using Irsa.Repository.Brand;
using System.Threading.Tasks;

namespace Irsa.Repository.Wrapper
{

    public class RepositoryWrapper : IRepositoryWrapper
    {

        private RepositoryContext _repoContext;

        //Brand
        private IBaggageRepository _baggage;
        public IBaggageRepository Baggage
        {
            get
            {
                if (_baggage == null)
                {
                    _baggage = new BaggageRepository(_repoContext);
                }

                return _baggage;
            }
        }
        public RepositoryWrapper(RepositoryContext RepositoryContext)
        {
            _repoContext = RepositoryContext;
        }


        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }

}
