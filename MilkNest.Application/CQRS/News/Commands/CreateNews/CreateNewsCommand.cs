﻿using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.News.Commands.CreateNews
{
    public class CreateNewsCommand:IRequest<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public  List<IFormFile> Images { get; set; } 
    }
}

