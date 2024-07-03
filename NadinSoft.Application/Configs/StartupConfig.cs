using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NadinSoft.Application.CQRS.Products.Queries.GetProduct;
using NadinSoft.Application.Profiles.Users;
using NadinSoft.Application.Services.Authentication;
using NadinSoft.Application.Validations.Products;
using NadinSoft.Common.DTOs;
using System.Text;

namespace NadinSoft.Application.Configs
{
    public static class StartupConfig
    {
        public static void AddMediatRConfig(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetProductQuery).Assembly);
            });
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile).Assembly);
        }

        public static void AddValidations(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(CreateProductCommandValidation).Assembly);
        }

        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IJwtService, JwtService>();

            services.Configure<AthenticationConfigDto>(configuration.GetSection("Authentication"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = configuration["Authentication:Jwt:Issuer"],
                        ValidAudience = configuration["Authentication:Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:Jwt:IssuerSigningKey"])),
                        RequireExpirationTime = true
                    };
                });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build();
            });

        }
    }
}
