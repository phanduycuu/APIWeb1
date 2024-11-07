using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface ITokenRepository
    {
        Task<string> CreateToken(AppUser user);
    }
}
