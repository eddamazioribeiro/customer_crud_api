using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApp.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Repository
{
    public class CustomerRepository
    {
        private readonly CustomerContext _context;

        public CustomerRepository(CustomerContext context)
        {
            _context = context;
        }

        public void Create(Customer customer)
        {
            _context.Add(customer);
        }

        public void Update(Customer customer)
        {
            _context.Update(customer);
        }

        public void Delete(Customer customer)
        {
            _context.Remove(customer);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Customer> GetCustomerById(int id, bool getAddresses = false)
        {
            IQueryable<Customer> query = _context.Customers;

            query.Where(c => c.Id == id);

            if(getAddresses)
            {
                query.Include(c => c.Addresses);
            }

            return await query.FirstOrDefaultAsync<Customer>();
        }

        public async Task<List<Customer>> GetAllCustomersByNameAsync(string name, bool getAddresses = false)
        {
            List<Customer> customers = new List<Customer>();
            IQueryable<Customer> query = _context.Customers;

            if(getAddresses)
            {
                query.Include(c => c.Addresses);
            }

            query.Where(c => c.Name.ToLower().Contains(name.ToLower()));

            customers = await query.ToListAsync<Customer>();

            return customers;
        }

        public async Task<List<Customer>> GetAllCustomersAsync(bool getChildren = false)
        {
            List<Customer> customers = new List<Customer>();
            IQueryable<Customer> query = _context.Customers;

            if(getChildren)
            {
                query.Include(c => c.Addresses);
            }

            customers = await query.ToListAsync<Customer>();

            return customers;
        }
    }
}