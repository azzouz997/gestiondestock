using ElamanaTakaful.Domain.Entities;
using ElamanaTakaful.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElamanaTakaful.Application.Services.ProductServices
{
    public class ProductRepository
    {
        private readonly ElamanaTakafulContext _context;
        public ProductRepository(ElamanaTakafulContext context)
        {
            _context = context;
        }

        public List<Product> FindProductByName(string searchProductName)
        {
            var products = from p in _context.Products select p;
            
            if (!String.IsNullOrEmpty(searchProductName))
            {
                products = products.Where(p => p.ProductName.ToLower().Contains(searchProductName.ToLower()))
                                   .Include(p => p.Supplier.SupplierId);
            }
            
            return products.ToList();

        }

        public List<Product> FindProductByCode(string searchProductCode)
        {
            var products = from p in _context.Products select p;

            if (!String.IsNullOrEmpty(searchProductCode))
            {
                products = products.Include(p => p.Supplier.SupplierId).Where(p => p.ProductCode.ToLower().Contains(searchProductCode.ToLower()));
            }

            return products.ToList();

        }


        public List<Product> FindProductBySupplier(string supplierCode)
        {
            var products = _context.Products.Include(p => p.Supplier).Where(p => p.Supplier.SupplierCode.ToLower() == supplierCode.ToLower()).ToList();
            return products;
        }

    }
}
