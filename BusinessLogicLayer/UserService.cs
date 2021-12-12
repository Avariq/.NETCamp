using AnimeLib.Domain.DataAccess;
using AnimeLib.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services
{
    public class UserService : IUserService
    {
        private readonly AnimeContext context;

        public UserService(AnimeContext _context)
        {
            context = _context;
        }

        public User GetUserByUsername(string username)
        {
            var user = context.Users
                .Include(u => u.Role)
                .Where(u => u.Username.Equals(username))
                .SingleOrDefault();

            return user;
        }
    }
}
