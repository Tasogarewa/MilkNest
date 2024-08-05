using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MilkNest.Application.Interfaces;
using MilkNest.Persistence.Repository;
using MilkNest.Persistence.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
namespace MilkNest.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");
            services.AddDbContext<MilkNestDbContext>(options =>
            {
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("MilkNest.Server")).UseLazyLoadingProxies();
                
            });
           

            services.AddRepositories();
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();
            var applicationAssembly = Assembly.Load("MilkNest.Application");
            var interfaces = applicationAssembly.GetTypes().Where(t => t.IsInterface && t.Name.EndsWith("Service")).ToList();
            var implementations = types.Where(t => t.IsClass && t.Name.EndsWith("Service")).ToList();

            foreach (var interfaceType in interfaces)
            {
                var implementationType = implementations.FirstOrDefault(t => t.Name == interfaceType.Name.Substring(1));
                if (implementationType != null)
                {
              
                    services.AddScoped(interfaceType, implementationType);
                }
            }
            return services;
        }

        public static void AddRepositories(this IServiceCollection services)
        {
          
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
