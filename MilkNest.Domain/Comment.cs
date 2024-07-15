using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Domain
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateUpdated { get; set; }
        public virtual ICollection<Comment>? Replies { get; set; } = new List<Comment>();
        public Guid? NewsId { get; set; }
        public virtual News? News { get; set; }
        public Guid? ProductId { get; set; }
        public virtual Product? Product { get; set; } 
        public Guid? ParentCommentId { get; set; }
        public virtual Comment? ParentComment { get; set; }

    }
}
