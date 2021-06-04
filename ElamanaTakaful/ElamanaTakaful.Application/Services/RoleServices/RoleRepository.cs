using ElamanaTakaful.Application.Common.Repositories;
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
    public class RoleRepository
    {
        private readonly ElamanaTakafulContext _context;
        private readonly IDataRepository<Role> _roleService;

        public RoleRepository(ElamanaTakafulContext context, IDataRepository<Role> roleService)
        {
            _context = context;
            _roleService = roleService;
        }

        //Not finished
        public void AddRoleFunctions(int roleId, List<Function> newFunctions)
        {
            Role role = _roleService.Get(roleId);
            foreach (Function f in newFunctions)
            {
                f.Roles.Add(role);
                role.Functions.Add(f);
            }
            _context.SaveChanges();
        }

        //Not finished
        public void DeleteRoleFunctions(int roleId, List<int> oldFunctionsIds)
        {
            Role role = _roleService.Get(roleId);
            var oldFunctions = _context.Functions.Where(f => oldFunctionsIds.Contains(f.FunctionId)).ToList();

            foreach (Function f in oldFunctions)
            {
                role.Functions.Remove(f);
                _context.Functions.Remove(f);
            }

            _context.SaveChanges();
        }

        public Role GetRoleByName(string roleName)
        {
            return _context.Roles.Where(r => r.Name.ToLower().Contains(roleName.ToLower())).FirstOrDefault();
        }

        public List<string> GetRolesByFunction(int functionId)
        {
            List<string> rolesResult = new();
            
            List<Role> roles = _context.Roles.Include(r => r.Functions).ToList();
            foreach (Role r in roles)
            {
                foreach(Function f in r.Functions)
                {
                    if(f.FunctionId == functionId)
                    {
                        rolesResult.Add(r.Name);
                    }
                }
            }

            return rolesResult;

        } 

    
    }
}
