

namespace MilkNest.Domain
{
    public class Image
    {
        public Guid Id { get; set; }
        public string Url { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<JobVacancy> JobVacancies { get; set; } = new List<JobVacancy>();
        public virtual ICollection<News> News { get; set; } = new List<News>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        
    }
}