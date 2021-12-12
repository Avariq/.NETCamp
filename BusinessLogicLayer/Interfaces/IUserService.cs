using AnimeLib.Domain.Models;

namespace AnimeLib.Services
{
    public interface IUserService
    {
        User GetUserByUsername(string username);
    }
}