using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NadinSoft.DataBase.Contexts;
using NadinSoft.Domain.Repositories;
using NadinSoft.Infrastructure.Repositories;

namespace NadinSoft.Infrastructure.Configs
{
    public static class StartupConfig
    {
        public static void AddDbContext(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<NadinSoftContext>(cfg => cfg.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository> ();
        }
    }
}
