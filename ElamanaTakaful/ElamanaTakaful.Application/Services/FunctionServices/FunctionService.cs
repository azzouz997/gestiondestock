using ElamanaTakaful.Application.Common.Repositories;
using ElamanaTakaful.Application.Services.RoleServices;
using ElamanaTakaful.Domain.Entities;
using ElamanaTakaful.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElamanaTakaful.Application.Services.FunctionServices
{
    public class FunctionService : IDataRepository<Function>
    {
        readonly ElamanaTakafulContext _context;

        public FunctionService(ElamanaTakafulContext context)
        {
            _context = context;
        }

        public Function Add(Function function)
        {
            function.CreationDate = DateTime.Now;  
            _context.Functions.Add(function);
            _context.SaveChanges();
            return function;
        }

        public void Delete(int id)
        {
            var function = _context.Functions.FirstOrDefault(x => x.FunctionId == id);
            if (function != null)
            {
                _context.Remove(function);
                _context.SaveChanges();
            }
        }

        public Function Get(int id)
        {
            return _context.Functions.FirstOrDefault(x => x.FunctionId == id);
        }

        public List<Function> GetAll()
        {
            return _context.Functions.ToList();
        }

        public void Update(Function function)
        {
            _context.Functions.Update(function);
            _context.SaveChanges();
        }


    }
}
