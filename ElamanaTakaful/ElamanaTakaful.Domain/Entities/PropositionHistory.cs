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
    public class PropositionHistory
    {
        public PropositionHistory()
        {
            Proposition = new();
        }

        private PropositionHistory(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
        private ILazyLoader LazyLoader { get; set; }
        private Proposition _proposition;

        [Key]
        public int PropositionHistoryId { get; set; }
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
        public int ProductId { get; set; }
        public int SupplierId { get; set; }


        public int PropositionId { get; set; }
        [JsonIgnore]
        public virtual Proposition Proposition
        {
            get => LazyLoader.Load(this, ref _proposition);
            set => _proposition = value;
        }


    }
}
