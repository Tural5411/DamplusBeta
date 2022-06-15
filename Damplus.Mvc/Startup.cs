using AutoMapper;
using Damplus.Entities.Concrete;
using Damplus.Mvc.Areas.Admin.Helpers.Abstract;
using Damplus.Mvc.Areas.Admin.Helpers.Concrete;
//using Damplus.Mvc.AutoMapper.Profiles;
using Damplus.Services.AutoMapper.Profiles;
using Damplus.Services.Extensions;
using Damplus.Shared.Utilities.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Damplus.Mvc
{
    public class Startup
    {
        [Obsolete]
        public Startup(IConfiguration configuration, Microsoft.Extensions.Hosting.IHostingEnvironment env)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.Json", true, true)
                .AddJsonFile("appsettings.Development.Json", true, true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFileHelper, FileHelper>();
            services.AddTransient<IImageHelper, ImageHelper>();
            //services.AddSingleton(provider => new MapperConfiguration(cfg =>
            // {
            //     cfg.AddProfile(new UserProfile(provider.GetService<IImageHelper>()));
            //     cfg.AddProfile(new TeamProfile(provider.GetService<IImageHelper>()));
            //     cfg.AddProfile(new ArticleProfile());
            //     cfg.AddProfile(new ViewModelsProfile(provider.GetService<IImageHelper>()));
            // }).CreateMapper());
            services.Configure<AboutUsPageInfo>(Configuration.GetSection("AboutUsPageInfo"));
            services.Configure<SmmPacket>(Configuration.GetSection("SmmPacket"));
            services.Configure<WebsiteInfo>(Configuration.GetSection("WebsiteInfo"));
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.Configure<SliderPageInfo>(Configuration.GetSection("SliderPageInfo"));
            services.Configure<ChooseUsPageInfo>(Configuration.GetSection("ChooseUsPageInfo"));
            services.Configure<ArticleRightSideBarWidgetOptions>(Configuration.GetSection("ArticleRightSideBarWidgetOptions"));
            services.ConfigureWritable<ArticleRightSideBarWidgetOptions>(Configuration.GetSection("ArticleRightSideBarWidgetOptions"));
            services.ConfigureWritable<WebsiteInfo>(Configuration.GetSection("WebsiteInfo"));
            services.ConfigureWritable<SmmPacket>(Configuration.GetSection("SmmPacket"));
            services.ConfigureWritable<AboutUsPageInfo>(Configuration.GetSection("AboutUsPageInfo"));
            services.ConfigureWritable<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.ConfigureWritable<SliderPageInfo>(Configuration.GetSection("SliderPageInfo"));
            services.ConfigureWritable<ChooseUsPageInfo>(Configuration.GetSection("ChooseUsPageInfo"));
            services.AddControllersWithViews(options =>
            {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Bu sah? boþ ola bilm?z");
                //options.Filters.Add<MvcExceptionFilter>();
            }).AddRazorRuntimeCompilation().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            }).AddNToastNotifyToastr();

            services.AddSession();
            services.AddAutoMapper(typeof(CategoryProfile),/*typeof(PriceProfile), typeof(ArticleProfile),typeof(BannerProfile), typeof(UserProfile), typeof(CommentProfile), typeof(ViewModelsProfile), typeof(BusinessProfile),typeof(BusinessCategoryProfile), typeof(TeamProfile),*/ typeof(ProjectCategoryProfile), typeof(ProjectProfile));
            services.LoadMyServices(connectionString: Configuration["Data:DefaultConnection:ConnectionString"]);
            //services.LoadMyServices(connectionString: Configuration.GetConnectionString("LocalDB"));

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/Auth/Login");
                options.LogoutPath = new PathString("/Admin/Auth/Logout");
                options.Cookie = new CookieBuilder
                {
                    Name = "Damplus",
                    HttpOnly = true,  //Cookiler front end terefinde gorsenmyecek!
                    SameSite = SameSiteMode.Strict, //Cookiler ferqli unvandan gelir
                    SecurePolicy = CookieSecurePolicy.SameAsRequest //Always!
                };
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7);
                options.AccessDeniedPath = new PathString("/Admin/Auth/AccesDenied");
            });
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseDeveloperExceptionPage();
            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseNToastNotify();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "article",
                    pattern: "bloq/{title}/{articleId}",
                    defaults: new { controller = "Article", action = "Detail" }
                    );
                endpoints.MapControllerRoute(
                    name: "service",
                    pattern: "xidmet/{name}/{serviceId}",
                    defaults: new { controller = "Services", action = "Detail" }
                    );
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
