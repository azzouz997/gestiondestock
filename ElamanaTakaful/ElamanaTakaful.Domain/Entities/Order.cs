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
    public class Order
    {
        public Order()
        {
            Creator = new();
            Validator = new();
            Proposition = new();
        }

        private ILazyLoader LazyLoader { get; set; }
        private Order(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        private User _validator;
        private User _creator;
        private Proposition _proposition;
        private List<OrderHistory> _orderHistory;


        [Key]
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        public DateTime CreationStartDate { get; set; }
        public DateTime CreationEndDate { get; set; }
        public DateTime ValidationStartDate { get; set; }
        public DateTime ValidationEndDate { get; set; }
        public string OrderStatus { get; set; }
        public float Quantity { get; set; }
        public string OrderReceiptId { get; set; }

        public int? CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        [JsonIgnore]
        public virtual User Creator
        {
            get => LazyLoader.Load(this, ref _creator);
            set => _creator = value;
        }

        public int? ValidatorId { get; set; }
        [ForeignKey("ValidatorId")]
        [JsonIgnore]
        public virtual User Validator
        {
            get => LazyLoader.Load(this, ref _validator);
            set => _validator = value;
        }

        public int PropositionId { get; set; }
        [ForeignKey("PropositionId")]
        [JsonIgnore]
        public virtual Proposition Proposition
        {
            get => LazyLoader.Load(this, ref _proposition);
            set => _proposition = value;
        }

        
        public virtual List<OrderHistory> OrderHistory
        {
            get => LazyLoader.Load(this, ref _orderHistory);
            set => _orderHistory = value;
        }


    }
}
