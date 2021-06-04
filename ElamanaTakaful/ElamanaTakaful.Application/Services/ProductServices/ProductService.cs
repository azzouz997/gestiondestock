using ElamanaTakaful.Application.Common.Repositories;
using ElamanaTakaful.Application.Services.SupplierServices;
using ElamanaTakaful.Domain.Entities;
using ElamanaTakaful.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ElamanaTakaful.Application.Services.ProductServices
{
    public class ProductService : IDataRepository<Product>
    {
        readonly ElamanaTakafulContext _context;
        readonly SupplierService _supplierService;
        
        public ProductService(ElamanaTakafulContext context)
        {
            _context = context;
            _supplierService = new SupplierService(_context);
        }

        public Product Add(Product product)
        {
            Supplier supplier = _supplierService.Get(product.SupplierId);
            product.CreationDate = DateTime.Now;
            product.Supplier = supplier;
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public void Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
            if (product != null)
            {
                _context.Remove(product);
                _context.SaveChanges();
            }
        }

        public Product Get(int id)
        {
            return _context.Products.FirstOrDefault(x => x.ProductId == id);
        }

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public void Update(Product product)
        {
            Product productPreviousData = _context.Products.Include(p => p.Supplier).Include(p => p.ProductHistory).FirstOrDefault(x => x.ProductId == product.ProductId);
            _context.Entry(productPreviousData).State = EntityState.Detached;

            List<ProductHistory> arr = productPreviousData.ProductHistory;
            ProductHistory productHistory = new();
            productHistory.ProductId = productPreviousData.ProductId;
            productHistory.ProductCode = productPreviousData.ProductCode;
            productHistory.ProductName = productPreviousData.ProductName;
            productHistory.QuantityInStock = productPreviousData.QuantityInStock;
            productHistory.QuantityUsed = productPreviousData.QuantityUsed;
            productHistory.LastBuyingDate = productPreviousData.LastBuyingDate;
            productHistory.CreationDate = productPreviousData.CreationDate;
            productHistory.Status = productPreviousData.Status;
            productHistory.SupplierId = productPreviousData.SupplierId;

            arr.Add(productHistory);
            product.ProductHistory = arr;

            Supplier supplier = _supplierService.Get(product.SupplierId);
            product.Supplier = supplier;
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
