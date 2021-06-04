using ElamanaTakaful.Application.Common.Repositories;
using ElamanaTakaful.Domain.Entities;
using ElamanaTakaful.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ElamanaTakaful.Application.Services.SupplierServices
{
    public class SupplierService : IDataRepository<Supplier>
    {
        readonly ElamanaTakafulContext _context;
        public SupplierService(ElamanaTakafulContext context)
        {
            _context = context;
        }

        public Supplier Add(Supplier supplier)
        {
            supplier.CreationDate = DateTime.Now;
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
            return supplier;
        }

        public void Delete(int id)
        {
            var supplier = _context.Suppliers.FirstOrDefault(x => x.SupplierId == id);
            if (supplier != null)
            {
                _context.Remove(supplier);
                _context.SaveChanges();
            }
        }

        public Supplier Get(int id)
        {
            return _context.Suppliers.FirstOrDefault(x => x.SupplierId == id);
        }

        public List<Supplier> GetAll()
        {
            return _context.Suppliers.ToList();
        }

        public void Update(Supplier supplier)
        {
            Supplier supplierPreviousData = _context.Suppliers.Include(u => u.SupplierHistory).FirstOrDefault(x => x.SupplierId == supplier.SupplierId);
            _context.Entry(supplierPreviousData).State = EntityState.Detached;

            List<SupplierHistory> arr = supplierPreviousData.SupplierHistory;

            SupplierHistory supplierHistory = new();
            supplierHistory.SupplierId = supplierPreviousData.SupplierId;
            supplierHistory.SupplierCode = supplierPreviousData.SupplierCode;
            supplierHistory.Name = supplierPreviousData.Name;
            supplierHistory.PhoneNumber = supplierPreviousData.PhoneNumber;
            supplierHistory.Address = supplierPreviousData.Address;
            supplierHistory.OrdersNumber = supplierPreviousData.OrdersNumber;
            supplierHistory.LastBuyDate = supplierPreviousData.LastBuyDate;
            supplierHistory.CreationDate = supplierPreviousData.CreationDate;

            arr.Add(supplierHistory);
            supplier.SupplierHistory = arr;

            _context.Suppliers.Update(supplier);
            _context.SaveChanges();
        }
    }
}
