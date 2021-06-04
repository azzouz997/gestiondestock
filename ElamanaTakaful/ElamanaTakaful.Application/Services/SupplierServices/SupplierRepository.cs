using ElamanaTakaful.Domain.Entities;
using ElamanaTakaful.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElamanaTakaful.Application.Services.SupplierServices
{
    public class SupplierRepository
    {
        private readonly ElamanaTakafulContext _context;

        public SupplierRepository(ElamanaTakafulContext context)
        {
            _context = context;
        }

        public List<Supplier> FindSupplierByName(string searchSupplierName)
        {
            var suppliers = _context.Suppliers.Where(s => s.Name.ToLower() == searchSupplierName.ToLower()).ToList();
            return suppliers;
        }

        public List<Supplier> FindSupplierByCode(string searchSupplierCode)
        {
            var suppliers = _context.Suppliers.Where(s => s.SupplierCode.ToLower() == searchSupplierCode.ToLower()).ToList();
            return suppliers;
        }
    }
}
