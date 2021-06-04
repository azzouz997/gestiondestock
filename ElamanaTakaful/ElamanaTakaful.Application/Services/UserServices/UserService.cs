using ElamanaTakaful.Application.Common.Repositories;
using ElamanaTakaful.Application.Services.RoleServices;
using ElamanaTakaful.Domain.Entities;
using ElamanaTakaful.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ElamanaTakaful.Application.Services.UserServices
{
    public class UserService : IDataRepository<User>
    {
        readonly ElamanaTakafulContext _context;
        readonly IDataRepository<Role> _roleService;

        public UserService(ElamanaTakafulContext context, IDataRepository<Role> roleService)
        {
            _context = context;
            _roleService = roleService;
        }

        public User Add(User user)
        {
            user.PasswordsHistory.Add(new PasswordHistory { Password = user.Password, EntryDate = DateTime.Now });

            byte[] data = Encoding.ASCII.GetBytes(user.Password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = Encoding.ASCII.GetString(data);
            user.Password = hash;

            Role userRole = _roleService.Get(user.Role.RoleId);
            user.Role = userRole;
            user.CreationDate = DateTime.Now;

            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);
            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChanges();
            }
        }

        public User Get(int id)
        {
            var user = _context.Users.Include(u => u.Role).ThenInclude(r => r.Functions).FirstOrDefault(x => x.UserId == id);
            return user;
        }

        public List<User> GetAll()
        {
            return _context.Users.Include(u => u.Role).ThenInclude(r => r.Functions).ToList();
        }

        public void Update(User user)
        {
            User userToBeUpdated = Get(user.UserId);

            List<PasswordHistory> arr = _context.Users.Include(u => u.PasswordsHistory).FirstOrDefault(x => x.UserId == user.UserId).PasswordsHistory;
            int longeur = arr.Count;
            string password = arr[longeur - 1].Password;

            byte[] data = Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = Encoding.ASCII.GetString(data);
            userToBeUpdated.Password = hash;
            userToBeUpdated.PasswordsHistory = arr;

            userToBeUpdated.Name = user.Name;
            userToBeUpdated.Email = user.Email;
            userToBeUpdated.Description = user.Description;
            userToBeUpdated.Direction = user.Direction;
            userToBeUpdated.ModifyDate = DateTime.Now;
            userToBeUpdated.RoleId = user.Role.RoleId;

            _context.Users.Update(userToBeUpdated);
            _context.SaveChanges();
        }

    }
}
