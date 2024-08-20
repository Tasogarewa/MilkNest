using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Domain
{
    public class News
    {
        public Guid Id { get; set; }
        
    
        public DateTime PublishDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<NewsLocalization> Localizations { get; set; } = new List<NewsLocalization>();
    }
    public class NewsLocalization
    {
        public NewsLocalization()
        {
        }

        public Guid Id { get; set; }
        public Guid NewsId { get; set; }
        public string Language { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual News News { get; set; }
    }
}
