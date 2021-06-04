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
    public class OrderHistory
    {
        public OrderHistory()
        {
            Order = new();
        }

        private OrderHistory(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
        private ILazyLoader LazyLoader { get; set; }
        private Order _order;

        [Key]
        public int OrderHistoryId { get; set; }
        public int OrderNumber { get; set; }
        public DateTime CreationStartDate { get; set; }
        public DateTime CreationEndDate { get; set; }
        public DateTime ValidationStartDate { get; set; }
        public DateTime ValidationEndDate { get; set; }
        public string OrderStatus { get; set; }

        public int? ValidatorId { get; set; }
        public int? CreatorId { get; set; }
        public int? PropositionId { get; set; }

        public int OrderId { get; set; }
        [JsonIgnore]
        public virtual Order Order
        {
            get => LazyLoader.Load(this, ref _order);
            set => _order = value;
        }

    }
}
