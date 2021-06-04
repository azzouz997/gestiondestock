using ElamanaTakaful.Application.Common.Repositories;
using ElamanaTakaful.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ElamanaTakaful.Infrastructure.Contexts;

namespace ElamanaTakaful.Application.Services.UserServices
{
    public class UserRepository
    {
        private readonly ElamanaTakafulContext _context;
        private readonly IDataRepository<User> _userService;

        public UserRepository(ElamanaTakafulContext context, IDataRepository<User> userService)
        {
            _context = context;
            _userService = userService;
        }

        public User GetUserByCred(UserCred userCred)
        {
            bool notFound = true;
            List<User> users = _userService.GetAll();
            User user = new();

            byte[] data = Encoding.ASCII.GetBytes(userCred.Password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = Encoding.ASCII.GetString(data);

            while (notFound)
            {
                foreach (User u in users)
                    if (u.Login == userCred.Login && u.Password == hash)
                    {
                        user = u;
                        notFound = false;

                    }
            }

            return user;
        }

        public User GetUserByLogin(string login)
        {
            bool notFound = true;
            List<User> users = _userService.GetAll();
            User user = new();

            while (notFound)
            {
                foreach (User u in users)
                    if (u.Login == login)
                    {
                        user = u;
                        notFound = false;

                    }
            }

            return user;
        }

        public void UpdateUserPassword(User user, string oldPassword)
        {
            byte[] data = Encoding.ASCII.GetBytes(oldPassword);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = Encoding.ASCII.GetString(data);

            User oldUser = _userService.Get(user.UserId);
            _context.Entry(oldUser).State = EntityState.Detached;
            //_context.Entry(oldUser.Role).State = EntityState.Detached;
            //_context.Entry(oldUser.Role.Functions).State = EntityState.Detached;

            if (oldUser.Password == hash)
            {
                byte[] data1 = Encoding.ASCII.GetBytes(user.Password);
                data1 = new System.Security.Cryptography.SHA256Managed().ComputeHash(data1);
                String hash1 = Encoding.ASCII.GetString(data1);

                user.PasswordsHistory.Add(new PasswordHistory { Password = oldPassword, EntryDate = DateTime.Now });
                user.Password = hash1;

                int rId = user.RoleId.GetValueOrDefault();
                Role role = GetRole(rId);
                user.Role = role;


                _context.Users.Update(user);
                _context.SaveChanges();
            }

            else
                throw new InvalidOperationException("Password not matched");
        
        }

        public void DeactivateUser(int id)
        {
            User user = _userService.Get(id);
            user.UserStatus = false;
            user.DeactivateDate = DateTime.Now;
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void ActivateUser(int id)
        {
            User user = _userService.Get(id);
            user.UserStatus = true;
            user.DeactivateDate = DateTime.Now;
            _context.Users.Update(user);
            _context.SaveChanges();
        }


        internal Role GetRole(int id)
        {
            return _context.Roles.FirstOrDefault(x => x.RoleId == id);
        }


    }
}
