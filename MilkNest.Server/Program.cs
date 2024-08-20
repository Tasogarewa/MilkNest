using DeepL;
using dotenv.net;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MilkNest.Application;
using MilkNest.Application.Common.Mapping;
using MilkNest.Domain;
using MilkNest.Persistence;
using MilkNest.Server.Middleware;
using System.Globalization;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace MilkNest.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddHealthChecks();
            builder.Services.AddResponseCompression();
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddControllersWithViews()
            .AddViewLocalization()
            .AddDataAnnotationsLocalization();
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                new CultureInfo("uk"),
                new CultureInfo("en-Us"),
                new CultureInfo("es"),
                new CultureInfo("de"),
                new CultureInfo("fr"),
                new CultureInfo("ja"),
                new CultureInfo("zh-cn"),
                new CultureInfo("pt")
               };

                options.DefaultRequestCulture = new RequestCulture("uk");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            var envFilePath = Path.Combine(Directory.GetCurrentDirectory(), "MilkNestEnv.env");
            DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { envFilePath }));

        

            builder.Configuration.AddEnvironmentVariables();

         
            builder.Configuration.AddInMemoryCollection(new Dictionary<string, string>
            {
                {"JWT:Key", Environment.GetEnvironmentVariable("JWT__KEY")},
                {"Authentication:Facebook:AppId", Environment.GetEnvironmentVariable("Authentication__Facebook__AppId")},
                {"Authentication:Facebook:AppSecret", Environment.GetEnvironmentVariable("Authentication__Facebook__AppSecret")},
                {"Authentication:Google:ClientId", Environment.GetEnvironmentVariable("Authentication__Google__ClientId")},
                {"Authentication:Google:ClientSecret", Environment.GetEnvironmentVariable("Authentication__Google__ClientSecret")},
                {"Authentication:GitHub:ClientId", Environment.GetEnvironmentVariable("Authentication__GitHub__ClientId")},
                {"Authentication:GitHub:ClientSecret", Environment.GetEnvironmentVariable("Authentication__GitHub__ClientSecret")},
                {"Twilio:AccountSID", Environment.GetEnvironmentVariable("Twilio__AccountSID")},
                {"Twilio:AuthToken", Environment.GetEnvironmentVariable("Twilio__AuthToken")},
                {"Twilio:PhoneNumber", Environment.GetEnvironmentVariable("Twilio__PhoneNumber")},
                {"DeepL:ApiKey", Environment.GetEnvironmentVariable("DeepL__ApiKey")}
              });
            builder.Services.AddSingleton<ITranslator>(provider =>
            {
                var apiKey = builder.Configuration["DeepL:ApiKey"];
                return new Translator(apiKey);
            });
            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();

                });
            });
            builder.Services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
            });
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplication();
            builder.Services.AddPersistance(builder.Configuration);

            builder.Services.AddSwaggerGen();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Tokens.EmailConfirmationTokenProvider = "Email";
                options.Tokens.ChangeEmailTokenProvider = "Email";
                options.Tokens.AuthenticatorTokenProvider = "Phone";
                options.Tokens.ChangePhoneNumberTokenProvider = "Phone";
            });


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {

                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); 
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            })
          .AddEntityFrameworkStores<MilkNestDbContext>()
          .AddDefaultTokenProviders();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(300); 
                options.Cookie.HttpOnly = true; 
                options.Cookie.IsEssential = true; 
            });
       
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
             {

                 options.SlidingExpiration = true;
                 options.ExpireTimeSpan = TimeSpan.FromMinutes(30); 
                 options.Cookie.HttpOnly = true; 
                 options.Cookie.SecurePolicy = CookieSecurePolicy.Always; 
                 options.Cookie.SameSite = SameSiteMode.Strict; 
                 options.Cookie.Name = "MilkNestCookie"; 
                 options.Cookie.MaxAge = TimeSpan.FromMinutes(60); 
             })
             .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = builder.Configuration["Jwt:Issuer"],
                     ValidAudience = builder.Configuration["Jwt:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                 };
             }).
            AddOpenIdConnect("Google", options =>
            {
                options.SignInScheme = IdentityConstants.ExternalScheme;
                options.SignOutScheme = IdentityConstants.ExternalScheme;
                options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                options.Authority = "https://accounts.google.com";
                options.ResponseType = "code";
                options.SaveTokens = true;
                options.CallbackPath = "/signin-google";
                options.Scope.Add("profile");
                options.Scope.Add("email");
               
            }).AddOAuth("GitHub", options =>
            {
              
                options.SignInScheme = IdentityConstants.ExternalScheme;
                options.ClientId = builder.Configuration["Authentication:GitHub:ClientId"];
                options.ClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"];
                options.CallbackPath = "/oauth/github-cb";
                options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                options.TokenEndpoint = "https://github.com/login/oauth/access_token";
                options.UserInformationEndpoint = "https://api.github.com/user";
                options.SaveTokens = true;
                options.ClaimActions.MapJsonKey("sub", "id");
                options.ClaimActions.MapJsonKey(ClaimTypes.Name, "login");
                options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                options.ClaimActions.MapJsonKey("urn:github:login", "login");
                options.ClaimActions.MapJsonKey("urn:github:url", "html_url");
                options.ClaimActions.MapJsonKey("urn:github:avatar", "avatar_url");
                options.Events = new OAuthEvents
                {
                    OnCreatingTicket = async context =>
                    {

                        var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                        var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                        response.EnsureSuccessStatusCode();
                        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                        context.RunClaimActions(json.RootElement);
                    }
                };
            }).AddFacebook(options =>
            {
               
                options.SignInScheme = IdentityConstants.ExternalScheme;
                options.AppId = builder.Configuration["Authentication:Facebook:AppId"];
                options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
                options.Scope.Add("email");
                options.Fields.Add("name");
                options.Fields.Add("email");
                options.SaveTokens = true;
                options.CallbackPath = "/signin-facebook";
            });


            builder.Services.AddAuthorization();

            builder.Services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new AssemblyMappingProfile(typeof(MilkNestDbContext).Assembly));
                opt.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            });

            var app = builder.Build();
          
            app.Use(next => context =>
            {
                var tokens = context.RequestServices.GetService<IAntiforgery>().GetAndStoreTokens(context);
                context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions { HttpOnly = false });
                return next(context);
            });
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<MilkNestDbContext>();
                    await context.Database.MigrateAsync();

                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    await SeedRolesAndAdminUser(roleManager, userManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSwagger();
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRequestLocalization();
            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCustomExceptionHandler();
            app.UseResponseCaching();
            //app.UseRateLimitingMiddleware();
            app.UseLocalizationMiddleware();
            app.UseRequestLoggingMiddleware();
            app.MapControllers();
            app.UseResponseCompression();
            app.UseHealthChecks("/health");
            app.UseHsts();
            app.MapFallbackToFile("/index.html");
            app.UseRequestLocalization();
            await app.RunAsync();
        }

        private static async Task SeedRolesAndAdminUser(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminUser = new ApplicationUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com"
            };

            string adminPassword = "Admin123!";
            var user = await userManager.FindByEmailAsync(adminUser.Email);

            if (user == null)
            {
                var createAdminUser = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdminUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
   

}