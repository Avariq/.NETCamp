using AnimeLib.Domain.Models;

namespace AnimeLib.Services
{
    public interface IUserService
    {
        User GetUserByUsername(string username);
        User GetUserById(int userId);
        User CreateUser(string username, string email, string passwordHash);
    }
}