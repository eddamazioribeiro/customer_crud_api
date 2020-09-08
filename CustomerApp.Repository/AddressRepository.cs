using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApp.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Repository
{
    public class AddressRepository
    {
        private readonly Context _context;

        public AddressRepository(Context context)
        {
            _context = context;
        }

        public void Create(Address address)
        {
            _context.Addresses.Add(address);
        }

        public void Update(Address address)
        {
            _context.Addresses.Update(address);
        }

        public void Delete(Address address)
        {
            _context.Addresses.Remove(address);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Address> GetAddressById(int id)
        {
            IQueryable<Address> query = _context.Addresses;

            query = query.Where(a => a.Id == id);

            return await query.FirstOrDefaultAsync<Address>();
        }

        public async Task<List<Address>> GetAllAddresses(int? customerId)
        {
            List<Address> addresses = new List<Address>();
            IQueryable<Address> query = _context.Addresses;

            if(customerId != null && customerId != 0)
            {
                query = query.Where(a => a.CustomerId == customerId);
            }

            addresses = await query.ToListAsync<Address>();

            return addresses;
        }
    }
}