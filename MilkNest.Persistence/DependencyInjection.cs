using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MilkNest.Application.Interfaces;
using MilkNest.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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

            return services;
        }

        public static void AddRepositories(this IServiceCollection services)
        {
          
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
