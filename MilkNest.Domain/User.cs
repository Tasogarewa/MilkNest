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
       
            public int Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string PasswordHash { get; set; }
            public bool IsAdmin { get; set; }
            public DateTime DateRegistered { get; set; }
            public bool IsOnline { get; set; }

            public ICollection<Order> Orders { get; set; }
            public ICollection<Comment> Comments { get; set; }
        
    }
}
