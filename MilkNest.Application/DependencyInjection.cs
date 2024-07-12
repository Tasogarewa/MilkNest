using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MilkNest.Application.Common.Behaviors;
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
            services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(RetryBehavior<,>));

           
            return services;
        }
    }
}
