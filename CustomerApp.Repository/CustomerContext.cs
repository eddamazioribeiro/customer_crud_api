using Microsoft.EntityFrameworkCore;
using CustomerApp.Domain.Model;

namespace CustomerApp.Repository
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {

        }
    }
}
