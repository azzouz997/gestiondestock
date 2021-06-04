using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElamanaTakaful.Domain.Entities
{
    public class Supplier
    {
        public Supplier()
        {
            SupplierHistory = new();
        }

        private Supplier(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
        private ILazyLoader LazyLoader { get; set; }
        private List<SupplierHistory> _supplierHistory;

        [Key]
        public int SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int OrdersNumber { get; set; }
        public DateTime LastBuyDate { get; set; }
        public DateTime CreationDate { get; set; }
        
        public virtual List<SupplierHistory> SupplierHistory
        {
            get => LazyLoader.Load(this, ref _supplierHistory);
            set => _supplierHistory = value;
        }
        
    }
}
