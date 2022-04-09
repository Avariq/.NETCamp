using AnimeLib.Domain.DataAccess;
using AnimeLib.Domain.Models;
using AnimeLib.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

            if (user == null)
            {
                throw new NonexistentUserUsernameException(username);
            }

            return user;
        }

        public User GetUserById(int userId)
        {
            var user = context.Users
                .Where(u => u.Id.Equals(userId))
                .SingleOrDefault();

            if (user == null)
            {
                throw new NonexistentUserIdException(userId);
            }

            return user;
        }

        private bool isValidEmail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private bool isExistingEmail(string newEmail)
        {
            var email = context.Users
                .Where(u => u.Email.Equals(newEmail))
                .SingleOrDefault();

            return email != null;
        }

        private bool isExistingUsername(string username)
        {
            var usernameCount = context.Users
                .Where(u => u.Username.Equals(username))
                .Count();

            if (usernameCount > 0)
            {
                return true;
            }

            return false;
        }

        private int getRoleIdByRoleName(string roleName)
        {
            var role = context.UserRoles
                .Where(ur => ur.Role.Equals(roleName))
                .Single();

            int roleId = role.Id;

            return roleId;
        }

        public User CreateUser(string username, string email, string passwordHash)
        {
            if (isExistingUsername(username))
            {
                throw new UserWithSuchUsernameAlreadyExistsException(username);
            }

            if (isValidEmail(email) == false)
            {
                throw new InvalidEmailAddressException(email);
            }

            if (isExistingEmail(email))
            {
                throw new UserWithSuchEmailAlreadyExistsException(email);
            }

            int roleId = getRoleIdByRoleName("User");

            var user = new User();
            user.Username = username;
            user.Email = email;
            user.PasswordHash = passwordHash;
            user.RoleId = roleId;

            context.Users.Add(user);
            context.SaveChanges();

            return user;

        }
    }
}
