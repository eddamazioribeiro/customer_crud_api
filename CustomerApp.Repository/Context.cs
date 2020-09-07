using Microsoft.EntityFrameworkCore;
using CustomerApp.Domain.Model;

namespace CustomerApp.Repository
{
    public class Context : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
    }
}
