using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Domain
{
    public class JobVacancy
    {
        public Guid Id { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime PublishDate { get; set; }
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
        public virtual ICollection<JobVacancyLocalization> Localizations { get; set; } = new List<JobVacancyLocalization>();
    }
    public class JobVacancyLocalization
    {
        public JobVacancyLocalization()
        {
        }

        public Guid Id { get; set; }
        public Guid JobVacancyId { get; set; }
        public string Language { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual JobVacancy JobVacancy { get; set; }
    }
}
