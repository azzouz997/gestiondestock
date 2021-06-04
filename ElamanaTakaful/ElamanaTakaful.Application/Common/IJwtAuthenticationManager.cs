using ElamanaTakaful.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElamanaTakaful.Application.Common
{
    public interface IJwtAuthenticationManager
    {
        public List<User> Users { get; set; }
        string Authenticate(string userName, string password);
    }
}
