using Microsoft.AspNetCore.Identity;
using NadinSoft.DataBase.Contexts;
using NadinSoft.Domain.Entities.Roles;
using NadinSoft.Domain.Entities.Users;

namespace NadinSoft.API.Configs
{
    public static class StartupConfig
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<NadinSoftContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
                
                opt.User.RequireUniqueEmail = true;
            });
        }
    }
}
