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
    public class ProductHistory
    {
        public ProductHistory()
        {
            Product = new();
        }

        private ProductHistory(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
        private ILazyLoader LazyLoader { get; set; }
        private Product _product;


        [Key]
        public int ProductHistoryId { get; set; }

        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public float QuantityInStock { get; set; }
        public float QuantityUsed { get; set; }
        public DateTime LastBuyingDate { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Status { get; set; }
        public int SupplierId { get; set; }

        public int ProductId { get; set; }
        [JsonIgnore]
        public virtual Product Product
        {
            get => LazyLoader.Load(this, ref _product);
            set => _product = value;
        }

    }
}
