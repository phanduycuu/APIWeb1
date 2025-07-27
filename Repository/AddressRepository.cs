using APIWeb1.Data;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using static System.Net.Mime.MediaTypeNames;

namespace APIWeb1.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDBContext _context;
        public AddressRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Address> CreateAsync(Address add)
        {
            await _context.Address.AddAsync(add);
            await _context.SaveChangesAsync();
            return add;
        }
    }
}
