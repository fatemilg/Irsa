using Irsa.Models;
using Microsoft.EntityFrameworkCore;

namespace Irsa.EFCore
{
    public class RepositoryContext : DbContext
    {

        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }
        //Models
        public DbSet<Baggage> Baggages { get; set; }


    }
}
