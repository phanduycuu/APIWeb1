using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface ITokenRepository
    {
        string CreateToken(AppUser user);
    }
}
