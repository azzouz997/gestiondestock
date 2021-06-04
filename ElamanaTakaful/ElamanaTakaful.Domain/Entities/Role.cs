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
    public class Role
    {
        public Role()
        {
            Functions = new();
        }


        private Role(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        private ILazyLoader LazyLoader { get; set; }
        private List<Function> _functions;
        private List<User> _users;

        [Key]
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public virtual List<User> Users
        {
            get => LazyLoader.Load(this, ref _users);
            set => _users = value;
        }

        public virtual List<Function> Functions
        {
            get => LazyLoader.Load(this, ref _functions);
            set => _functions = value;
        }
    }
}
