using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.JobVacancy.Commands.CreateJobVacancy
{
    public class CreateJobVacancyCommand:IRequest<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public  List<IFormFile> Images { get; set; } 
    }
}
