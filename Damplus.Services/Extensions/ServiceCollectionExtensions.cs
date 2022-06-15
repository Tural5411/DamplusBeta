using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Damplus.Data.Abstract.UnitOfWorks;
using Damplus.Data.Concrete.UnitOfWork;
using Damplus.Entities.Concrete;
using Damplus.Services.Abstract;
using Damplus.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Damplus.Data.Concrete.EntityFramework.Context;

namespace Damplus.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection,string connectionString)
        {
            //AddScoped = Her request yarandiqda yeni bir instance yaranir ve bir request icersinde sadece bir dene obyekt istfd olunur
            //Transient = Her obyekt cagirisinda yeni bir instance yaradir,Scopedde ise sadece Request zamani 1defe yaranir
            serviceCollection.AddDbContext<DamplusContext>(options=>options.UseSqlServer(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            serviceCollection.AddIdentity<User, Role>(options => {
                //User Password Options
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                //User UserName and Email Options
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<DamplusContext>();
            serviceCollection.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(15);
            });
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();
            serviceCollection.AddScoped<ICommentService, CommentManager>();
            serviceCollection.AddScoped<IBusinessService, BusinessManager>();
            serviceCollection.AddScoped<ITeamService, TeamManager>();
            serviceCollection.AddScoped<IProjectCategoryService, ProjectCategoryManager>();
            serviceCollection.AddScoped<IProjectService, ProjectManager>();
            serviceCollection.AddSingleton<IMailService, MailManager>();
            serviceCollection.AddScoped<IVideoService, VideoManager>();
            return serviceCollection;
        }
    }
}
