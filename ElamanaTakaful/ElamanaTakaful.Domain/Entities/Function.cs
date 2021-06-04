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
    public class Function
    {
        public Function()
        {
            Roles = new();
        }

        private ILazyLoader LazyLoader { get; set; }
        private Function(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        private List<Role> _roles;


        [Key]
        public int FunctionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreationDate { get; set; }
        public string route { get; set; }
 
        [JsonIgnore]
        public virtual List<Role> Roles
        {
            get => LazyLoader.Load(this, ref _roles);
            set => _roles = value;
        }


    }
}
