using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NadinSoft.Domain.Entities.Products;
using NadinSoft.Domain.Entities.Roles;
using NadinSoft.Domain.Entities.Users;
using NadinSoft.DataBase.Configuration;

namespace NadinSoft.DataBase.Contexts
{
    public class NadinSoftContext : IdentityDbContext<User,Role,int>
    {
        public NadinSoftContext(DbContextOptions<NadinSoftContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);

            base.OnModelCreating(builder);
        }

        public DbSet<Product> Products { get; set; }
    }
}
