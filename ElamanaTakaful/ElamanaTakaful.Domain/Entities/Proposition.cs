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
    public class Proposition
    {
        public Proposition()
        {
            Validator = new();
            QuoteHistory = new();
            Product = new();
            Supplier = new();
        }

        private ILazyLoader LazyLoader { get; set; }
        private Proposition(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
        private User _validator;
        private Product _product;
        private List<QuoteHistory> _quoteHistory;
        private Supplier _supplier;
        private List<PropositionHistory> _propositionHistory;

        [Key]
        public int PropositionId { get; set; }
        public int PropositionNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool PropositionStatus { get; set; }
        public DateTime ValidationDate { get; set; }
        public float AmountTTC { get; set; }
        public float AmountHT { get; set; }
        public string Direction { get; set; }
        public float Quantity { get; set; }
        public string QuoteId { get; set; }
        public string QuoteName { get; set; }

        public int? ValidatorId { get; set; }
        [ForeignKey("ValidatorId")]
        [JsonIgnore]
        public virtual User Validator
        {
            get => LazyLoader.Load(this, ref _validator);
            set => _validator = value;
        }

        public int ProductId { get; set; }
        [JsonIgnore]
        public virtual Product Product
        {
            get => LazyLoader.Load(this, ref _product);
            set => _product = value;
        }


        public int SupplierId { get; set; }
        [JsonIgnore]
        public virtual Supplier Supplier
        {
            get => LazyLoader.Load(this, ref _supplier);
            set => _supplier = value;
        }

        public virtual List<PropositionHistory> PropositionHistory
        {
            get => LazyLoader.Load(this, ref _propositionHistory);
            set => _propositionHistory = value;
        }

        public virtual List<QuoteHistory> QuoteHistory
        {
            get => LazyLoader.Load(this, ref _quoteHistory);
            set => _quoteHistory = value;
        }


    }
}
