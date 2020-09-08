using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApp.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Repository
{
    public class CustomerRepository
    {
        private readonly Context _context;

        public CustomerRepository(Context context)
        {
            _context = context;
        }

        public void Create(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            _context.Customers.Update(customer);
        }

        public void Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Customer> GetCustomerById(int id, bool getAddresses = false)
        {
            IQueryable<Customer> query = _context.Customers;

            if(getAddresses)
            {
                query = query.Include(c => c.Addresses);
            }

            query = query.Where(c => c.Id == id);

            return await query.FirstOrDefaultAsync<Customer>();
        }

        public async Task<bool> GetCustomerById(int id)
        {
            IQueryable<Customer> query = _context.Customers;

            query = query.Where(c => c.Id == id);
            var exists = await query.FirstOrDefaultAsync<Customer>();
            _context.Entry(exists).State = EntityState.Detached;

            return exists != null ? true : false;
        }        

        public async Task<List<Customer>> GetAllCustomersByNameAsync(string name, bool getAddresses = false)
        {
            List<Customer> customers = new List<Customer>();
            IQueryable<Customer> query = _context.Customers;

            if(getAddresses)
            {
                query = query.Include(c => c.Addresses);
            }

            query = query.Where(c => c.Name.ToLower().Contains(name.ToLower()));

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