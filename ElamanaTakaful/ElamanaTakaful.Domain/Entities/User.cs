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
    [Serializable]
    public class User
    {
        public User()
        {
            Role = new();
            PasswordsHistory = new();
        }

        private User(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        private ILazyLoader LazyLoader { get; set; }
        private Role _role;
        private List<PasswordHistory> _passwordsHistory;


        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Direction { get; set; }
        public bool UserStatus { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime DeactivateDate { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        public virtual List<PasswordHistory> PasswordsHistory
        {
            get => LazyLoader.Load(this, ref _passwordsHistory);
            set => _passwordsHistory = value;
        }

        public int? RoleId { get; set; }
        public virtual Role Role
        {
            get => LazyLoader.Load(this, ref _role);
            set => _role = value;
        }

    }
}
