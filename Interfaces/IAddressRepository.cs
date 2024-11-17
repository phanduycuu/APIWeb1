using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address> CreateAsync(Address add);
    }
}
