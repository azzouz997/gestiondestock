using ElamanaTakaful.Application.Common.Repositories;
using ElamanaTakaful.Application.Services.FunctionServices;
using ElamanaTakaful.Domain.Entities;
using ElamanaTakaful.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElamanaTakaful.Application.Services.RoleServices
{
    public class RoleService : IDataRepository<Role>
    {
        readonly IDataRepository<Function> _functionService;
        readonly ElamanaTakafulContext _context;

        public RoleService(ElamanaTakafulContext context, IDataRepository<Function> functionService)
        {
            _context = context;
            _functionService = functionService;
        }

        public Role Add(Role role)
        {
            List<Function> myFunctions = new();   
            foreach(Function f in role.Functions.ToList())
            {
                Function fun = _functionService.Get(f.FunctionId);
                myFunctions.Add(fun);
            }
            role.Functions = null;
            role.Functions = myFunctions;
            
            _context.Roles.Add(role);  
            _context.SaveChanges();
            return role;
        }

        public void Delete(int id)
        {
            var role = _context.Roles.Include(r=> r.Users).FirstOrDefault(x => x.RoleId == id);
            if (role != null)
            {
                _context.Remove(role);
                _context.SaveChanges();
            }
        }

        public Role Get(int id)
        {
            return _context.Roles.Include(r => r.Functions).FirstOrDefault(x => x.RoleId == id);
        }

        public List<Role> GetAll()
        {
            return _context.Roles.Include(r => r.Functions).ToList();
        }

        public void Update(Role role)
        {            
            Role rol = _context.Roles.Include(r => r.Functions).FirstOrDefault(x => x.RoleId == role.RoleId);

            List<Function> oldFunctions = rol.Functions;
            List<Function> newFunctions = role.Functions;

            rol.Name = role.Name;
            rol.Description = role.Description;
            _context.Entry(role).State = EntityState.Detached;

            List<int> existingIds = new();
            foreach (Function fu in oldFunctions)
            {
                existingIds.Add(fu.FunctionId);
            }

            List<int> newIds = new();
            foreach (Function f in newFunctions.ToList())
            {
                newIds.Add(f.FunctionId);
            }

            //adding new functions
            foreach (int i in newIds)
            {
                if (!existingIds.Contains(i))
                {

                    rol.Functions.Add(_functionService.Get(i));
                }
            }

            //deleting unwanted functions          
            foreach (int i in existingIds)
            {
                if (!newIds.Contains(i))
                {
                    rol.Functions.Remove(_functionService.Get(i));
                }
            }
          
            _context.Roles.Update(rol);
            _context.SaveChanges();
        }
    }
}
