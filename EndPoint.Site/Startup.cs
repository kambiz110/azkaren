using AspNetCoreRateLimit;
using AutoMapper;
using DNTCaptcha.Core;
using EndPoint.Site.Configurations;
using EndPoint.Site.Useful.Automapper;
using EndPoint.Site.Useful.IOC;
using EndPoint.Site.Useful.Ultimite;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Facad;
using Azmoon.Application.Service.Facad;
using Azmoon.Application.Service.Role.Command;
using Azmoon.Common.FileWork;
using Azmoon.Domain.Entities;
using Azmoon.Persistence.Contexts;
using Azmoon.Persistence.Helper;

using System.Reflection;
using Hangfire;
using Hangfire.SqlServer;
using EndPoint.Site.Helper.Hangfire;
using EndPoint.Site.Useful.Filter;
using Azmoon.Application.Service.Answer.Command;
using Azmoon.Application.Container;
using Azmoon.Persistence.Seeding;
using Azmoon.Application.Interfaces.QuizTemp;
using Azmoon.Application.Service.QuizTemp.Query;
using Azmoon.Application.Service.QuizTemp.Command;
using EndPoint.Site.Helper.ActionFilter;
using Azmoon.Application.Interfaces.WorkPlace;
using Azmoon.Application.Service.WorkPlace.Query;
using Azmoon.Application.Interfaces.Group;
using Azmoon.Application.Service.Group.Query;

namespace EndPoint.Site
{
    public class Startup
    {
        public static string WebRootPath { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          

            services.AddControllersWithViews();

            // Add Hangfire services.
            //services.AddHangfire(configuration => configuration
            //    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //    .UseSimpleAssemblyNameTypeSerializer()
            //    .UseRecommendedSerializerSettings()
            //    .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
            //    {
            //        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            //        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            //        QueuePollInterval = TimeSpan.Zero,
            //        UseRecommendedIsolationLevel = true,
            //        DisableGlobalLocks = true
            //    }));


            // services.AddHangfireServer();
            services.AddMvc(options =>
            {
                //options.Filters.Add(typeof(SetViewDataFilter));
            });
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);//You can set Time   
            });
           
        
            services.AddIdentity<User, Role>()
               .AddEntityFrameworkStores<DataBaseContext>()
               .AddDefaultTokenProviders()
               .AddRoles<Role>()
               .AddErrorDescriber<CustomIdentityError>()
               .AddPasswordValidator<MyPasswordValidator>();
            services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomUserClaimsPrincipalFactory>();
            services.Configure<IdentityOptions>(option =>
            {
                //UserSetting
                //option.User.AllowedUserNameCharacters = "abcd123";
                option.User.RequireUniqueEmail = true;

                //Password Setting
                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;//!@#$%^&*()_+
                option.Password.RequireUppercase = false;
                option.Password.RequiredLength = 6;
                option.Password.RequiredUniqueChars = 1;

                //Lokout Setting
                option.Lockout.MaxFailedAccessAttempts = 3;
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMilliseconds(10);

                //SignIn Setting
                option.SignIn.RequireConfirmedAccount = false;
                option.SignIn.RequireConfirmedEmail = false;
                option.SignIn.RequireConfirmedPhoneNumber = false;

            });
       

            services.AddDbContext<DataBaseContext>(
             p => p.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IDataBaseContext, DataBaseContext>();
      

            services.AddInjectRoleAplication();
            services.AddInjectWorkPlaceAplication();
            services.RegisterAutoMapper();
            services.AddDNTCaptcha(options =>
             options.UseCookieStorageProvider()
            .ShowThousandsSeparators(true)
             );
            //services.AddInitRateLimit(Configuration);

            services.AddScoped<Azmoon.Common.FileWork.IFileProvider, Azmoon.Common.FileWork.FileProvider>();
            services.AddTransient<IGetQuizTemp, GetQuizTemp>();
            services.AddTransient<IGetChildrenWorkPlace, GetChildrenWorkPlacees>();
            services.AddTransient<IGetChildrenGroup, GetChildrenGroup>();
            services.AddTransient<IAddQuizStartTemp, AddQuizStartTemp>();
            services.AddTransient<IGetWorkplacFirstToEndParent, GetWorkplacFirstToEndParent>();
            //FacadeInject
            services.AddScoped<IGroupFacad, GroupFacad>();
            services.AddScoped<IWorkPlaceFacad, WorkPlaceFacad>();
            services.AddScoped<IUserFacad, UserFacad>();
            services.AddScoped<IRoleFacad, RoleFacad>();
            services.AddScoped<IQuestionFacad, QuestionFacad>();
            services.AddScoped<IAnswerFacad, AnswerFacad>();
            services.AddScoped<IQuizFacad, QuizFacad>();
            services.AddScoped<IResultQuizFacad, ResultQuizFacad>();
            services.AddScoped<IQuizFilterFacad, QuizFilterFacad>();
            ApplicationConfigureServiceContainer.AddServices(services);


            // services.AddMediatR(typeof(Startup));

            //services.AddMediatR(Assembly.GetExecutingAssembly());
            /// filter 
            services.AddScoped<LayoutFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env )
        {

            //Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<DataBaseContext>();

                dbContext.Database.Migrate();

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseFastReport();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            // HangFire Dashboard
           // app.UseHangfireDashboard();
            //app.UseHangfireDashboard("/hangfire", new DashboardOptions
            //{
            //    Authorization = new[] { new HangfireAuthorizationFilter() }
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapHangfireDashboard();
            });
            WebRootPath = env.WebRootPath;
        }
       
    }
}
