using Microsoft.OpenApi.Models;
using NadinSoft.API.Middlewares;
using NadinSoft.DataBase.Contexts;
using NadinSoft.Infrastructure.Configs;
using NadinSoft.API.Configs;
using NadinSoft.Application.Configs;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
}); ;
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddAutoMapper();
builder.Services.AddMediatRConfig();
builder.Services.AddValidations();
builder.Services.AddJwt(builder.Configuration);
builder.Services.AddIdentity();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//#region EnsureDbCreated
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var dbContext = services.GetRequiredService<NadinSoftContext>();
//    if (dbContext.Database.CanConnect())
//    {
//        return;
//    }
//    dbContext.Database.EnsureCreated();
//}
//#endregion

//#region ApplyMissingMigrations
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var dbContext = services.GetRequiredService<NadinSoftContext>();

//    var appliedMigrations = dbContext.Database.GetAppliedMigrations().ToList();
//    var allMigrations = dbContext.Database.GetMigrations().ToList();

//    var missingMigrations = allMigrations.Where(x => !appliedMigrations.Contains(x)).ToList();

//    foreach (var missingMigration in missingMigrations)
//    {
//        try
//        {
//            var migrator = dbContext.GetInfrastructure().GetRequiredService<IMigrator>();
//            // Apply the missing migration
//            migrator.Migrate(missingMigration);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Error applying migration '{missingMigration}': {ex.Message}");
//        }
//    }
//}
//#endregion

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ValidationExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
