using MediatR;
using MilkNest.Application.CQRS.JobVacancy.Commands.CreateJobVacancy;
using MilkNest.Application.CQRS.JobVacancy.Commands.DeleteJobVacancy;
using MilkNest.Application.CQRS.JobVacancy.Commands.UpdateJobVacancy;
using MilkNest.Application.CQRS.JobVacancy.Queries.GetJobVacancies;
using MilkNest.Application.CQRS.User.Commands.CreateUser;
using MilkNest.Application.CQRS.User.Commands.DeleteUser;
using MilkNest.Application.CQRS.User.Commands.UpdateUser;
using MilkNest.Application.CQRS.User.Queries.GetUser;
using MilkNest.Application.CQRS.User.Queries.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.Interfaces
{
    public interface IJobVacancyService
    {
        public Task<Guid> CreateJobVacancyAsync(CreateJobVacancyCommand createJobVacancy);
        public Task<Unit> DeleteJobVacancyAsync(DeleteJobVacancyCommand deleteJobVacancy);
        public Task<Guid> UpdateJobVacancyAsync(UpdateJobVacancyCommand updateJobVacancy);
        public Task<JobVacancyListVm> GetJobVacanciesAsync(GetJobVacanciesQuery getJobVacancies);
    }
}
