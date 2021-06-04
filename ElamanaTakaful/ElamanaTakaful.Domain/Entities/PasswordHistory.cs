using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElamanaTakaful.Domain.Entities
{
    public class PasswordHistory
    {
        [Key]
        public int PasswordHistoryId { get; set; }
        public string Password { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
