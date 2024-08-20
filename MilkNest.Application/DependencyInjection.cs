using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using MilkNest.Application.Common.Behaviors;
using MilkNest.Application.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
           
            services.AddMediatR(cnfg => cnfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>),
             typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>),
             typeof(PerformanceBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>),
             typeof(CachingBehavior<,>));
          //  services.AddTransient(typeof(IPipelineBehavior<,>),
          //typeof(LocalizationBehavior<,>));
            services.AddMemoryCache();
            services.AddAutoMapper(opt =>
            {
                
                opt.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));

            });

            return services;
        }
    }
}
