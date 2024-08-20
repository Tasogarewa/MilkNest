using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Comment> Comments { get;set; } = new List<Comment>();
        public virtual ICollection<ProductLocalization> Localizations { get; set; } = new List<ProductLocalization>();
    }
    public class ProductLocalization
    {
        public ProductLocalization()
        {
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Language { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual Product Product { get; set; }
    }
}
