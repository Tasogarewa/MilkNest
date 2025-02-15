﻿using AutoMapper;
using MilkNest.Application.Common.Mapping;
using MilkNest.Application.Interfaces;
using MilkNest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.JobVacancy.Queries.GetJobVacancies
{
    public class JobVacancyDto:IMapWith<MilkNest.Domain.JobVacancy>
    {
       
        public JobVacancyDto()
        {
        }

      

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime PublishDate { get; set; }
        public  List<string> Images { get; set; }
        public string Language { get; set; }
        public async void Mapping(Profile profile)
        {
        
            profile.CreateMap<MilkNest.Domain.JobVacancy, JobVacancyDto>().
                ForMember(x=>x.UpdatedDate,opt=>opt.MapFrom(x=>x.UpdatedDate)).
                ForMember(x => x.Title, opt => opt.Ignore()).
                ForMember(x => x.Description, opt => opt.Ignore()).
                ForMember(x => x.PublishDate, opt => opt.MapFrom(x => x.PublishDate)).
                ForMember(x => x.Images, opt => opt.MapFrom(x => x.Images.Select(x=>x.Url).ToList()));
        }
    }
}
