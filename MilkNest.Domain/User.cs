using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MilkNest.Domain
{
    public class User
    {
       
            public Guid Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string PasswordHash { get; set; }
            public bool IsAdmin { get; set; }
            public DateTime DateRegistered { get; set; }
            public bool IsOnline { get; set; }
            public virtual Image? Image { get; set; } 
            public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();
        public virtual ICollection<Comment>? Comments { get; set; } = new List<Comment>();
        
    }
}
