using MilkNest.Application;
using MilkNest.Application.Common.Mapping;
using MilkNest.Persistence;
using MilkNest.Server.Middleware;
using System.Reflection;

namespace MilkNest.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(opt =>
            opt.AddPolicy("AllowAll", policy =>
            {
              policy.AllowAnyHeader();
              policy.AllowAnyMethod();
              policy.AllowAnyOrigin();
            }));
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
 );
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddApplication();
            builder.Services.AddPersistance(builder.Configuration);
            builder.Services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new AssemblyMappingProfile(typeof(MilkNestDbContext).Assembly));
                opt.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));

            });
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<MilkNestDbContext>();
                    await context.Database.EnsureCreatedAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapFallbackToFile("/index.html");
            app.UseCustomExceptionHandler();
            await app.RunAsync();
        }
    }
}