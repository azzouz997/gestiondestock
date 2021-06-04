using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ElamanaTakaful.Domain.Entities
{
    public class Product
    {
        public Product()
        {
            Supplier = new();
            Propositions = new();
        }

        private Product(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
        private ILazyLoader LazyLoader { get; set; }
        private Supplier _supplier;
        private List<ProductHistory> _productHistory;
        private List<Proposition> _propositions;

        [Key]
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public float QuantityInStock { get; set; }
        public float QuantityUsed { get; set; }
        public DateTime LastBuyingDate { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Status { get; set; }

        public int SupplierId { get; set; }
        public virtual Supplier Supplier
        {
            get => LazyLoader.Load(this, ref _supplier);
            set => _supplier = value;
        }

        [JsonIgnore]
        public virtual List<Proposition> Propositions
        {
            get => LazyLoader.Load(this, ref _propositions);
            set => _propositions = value;
        }

        public virtual List<ProductHistory> ProductHistory
        {
            get => LazyLoader.Load(this, ref _productHistory);
            set => _productHistory = value;
        }
    }
}
