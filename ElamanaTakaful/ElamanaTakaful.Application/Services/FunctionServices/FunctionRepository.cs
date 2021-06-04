using ElamanaTakaful.Domain.Entities;
using ElamanaTakaful.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElamanaTakaful.Application.Services.FunctionServices
{
    public class FunctionRepository
    {
        private readonly ElamanaTakafulContext _context;
        public FunctionRepository(ElamanaTakafulContext context)
        {
            _context = context;
        }

        public List<Function> GetFunctionsByRole(int roleId)
        {
            Role role = _context.Roles.Where(r => r.RoleId == roleId).FirstOrDefault();
            return role.Functions.Where(f => f.Status == true).ToList();
        }

        public List<Function> GetActiveFunctions()
        {
            return _context.Functions.Where(f => f.Status == true).ToList();
        }

        public Function ActivateFunction(int functionId)
        {
            Function function = _context.Functions.FirstOrDefault(x => x.FunctionId == functionId);
            function.Status = true;
            _context.Functions.Update(function);
            _context.SaveChanges();
            return function;
        }

        public Function DeactivateFunction(int functionId)
        {
            Function function = _context.Functions.FirstOrDefault(x => x.FunctionId == functionId);
            function.Status = false;
            _context.Functions.Update(function);
            _context.SaveChanges();
            return function;
        }


    }
}
