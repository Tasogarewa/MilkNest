using AutoMapper;
using MediatR;
using MilkNest.Application.Common.Exceptions;
using MilkNest.Application.CQRS.JobVacancy.Commands.CreateJobVacancy;
using MilkNest.Application.CQRS.JobVacancy.Commands.DeleteJobVacancy;
using MilkNest.Application.CQRS.JobVacancy.Commands.UpdateJobVacancy;
using MilkNest.Application.CQRS.JobVacancy.Queries.GetJobVacancies;
using MilkNest.Application.CQRS.News.Queries.GetNews;
using MilkNest.Application.Interfaces;
using MilkNest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Persistence.Services
{
    public class JobVacancyService : IJobVacancyService
    {
        private readonly IRepository<JobVacancy> _jobvacancyRepository;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;

        public JobVacancyService(IRepository<JobVacancy> jobvacancyRepository, IMapper mapper, IFileStorageService fileStorageService)
        {
            _jobvacancyRepository = jobvacancyRepository;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }

        public async Task<Guid> CreateJobVacancyAsync(CreateJobVacancyCommand createJobVacancy)
        {
            List<Image> images = new List<Image>();
            foreach (var image in createJobVacancy.Images)
            {
                images.Add(new Image() { Url = await _fileStorageService.SaveFileAsync(image) });
            }
            var JobVacancy = await _jobvacancyRepository.CreateAsync(new JobVacancy() { Description = createJobVacancy.Description, Title = createJobVacancy.Title, Images = images,  PublishDate = DateTime.Now });
            return JobVacancy.Id;
        }

        public async Task<Unit> DeleteJobVacancyAsync(DeleteJobVacancyCommand deleteJobVacancy)
        {
            var JobVacancy = await _jobvacancyRepository.GetAsync(deleteJobVacancy.Id);
            if (JobVacancy != null)
            {
                await _jobvacancyRepository.DeleteAsync(deleteJobVacancy.Id);
                return Unit.Value;
            }
            else
            {
                NotFoundException.Throw(JobVacancy, deleteJobVacancy.Id);
                return Unit.Value;
            }
        }

        public async Task<JobVacancyListVm> GetJobVacanciesAsync(GetJobVacanciesQuery getJobVacancies)
        {
            var JobVacancies = await _jobvacancyRepository.GetAllAsync();
            if (JobVacancies != null)
            {
                var jobVacanciesDtos = _mapper.ProjectTo<JobVacancyDto>(JobVacancies.AsQueryable()).ToList();
                return new JobVacancyListVm() { JobVacancyDtos  = jobVacanciesDtos };
            }
            else
            {
                NotFoundException.ThrowRange(JobVacancies);
                return null;
            }
        }

        public async Task<Guid> UpdateJobVacancyAsync(UpdateJobVacancyCommand updateJobVacancy)
        {
            var JobVacancy = await _jobvacancyRepository.GetAsync(updateJobVacancy.Id);
            List<Image> NewPhotos = new List<Image>();
            if (JobVacancy != null)
            {
                foreach (var img in JobVacancy.Images)
                {
                    _fileStorageService.DeleteFile(img.Url);
                }
                foreach (var img in updateJobVacancy.Images)
                {
                    JobVacancy.Images.Add(new Image() { Url = await _fileStorageService.SaveFileAsync(img) });
                }
                JobVacancy.UpdatedDate = DateTime.Now;
                JobVacancy.Title = updateJobVacancy.Title;
                JobVacancy.Description = updateJobVacancy.Description;
                await _jobvacancyRepository.UpdateAsync(JobVacancy);
                return JobVacancy.Id;
            }
            else
            {
                NotFoundException.Throw(JobVacancy, updateJobVacancy.Id);
                return Guid.Empty;
            }
        }
    }
}
