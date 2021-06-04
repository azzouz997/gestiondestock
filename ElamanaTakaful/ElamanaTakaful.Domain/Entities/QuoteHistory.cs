using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElamanaTakaful.Domain.Entities
{
    public class QuoteHistory
    {
        [Key]
        public int QuoteHistoryId { get; set; }
        public string QuoteFileId { get; set; }
        public string QuoteFileName { get; set; }
        public DateTime UptadeDate { get; set; }

    }
}
