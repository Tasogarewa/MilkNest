using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.JobVacancy.Commands.DeleteJobVacancy
{
    public class DeleteJobVacancyCommand:IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
