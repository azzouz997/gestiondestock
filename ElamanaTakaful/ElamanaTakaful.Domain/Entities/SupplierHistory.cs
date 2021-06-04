using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ElamanaTakaful.Domain.Entities
{
    public class SupplierHistory
    {
        public SupplierHistory()
        {
            Supplier = new();
        }

        private SupplierHistory(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
        private ILazyLoader LazyLoader { get; set; }
        private Supplier _supplier;

        [Key]
        public int SupplierHistoryId { get; set; }
        public string SupplierCode { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int OrdersNumber { get; set; }
        public DateTime LastBuyDate { get; set; }
        public DateTime CreationDate { get; set; }

        public int SupplierId { get; set; }
        [JsonIgnore]
        public virtual Supplier Supplier
        {
            get => LazyLoader.Load(this, ref _supplier);
            set => _supplier = value;
        }

    }
}
