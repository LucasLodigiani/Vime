using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Vime.Server.Common.Interfaces;
using Vime.Server.Domain.Entities;
using Vime.Server.Features.Rooms;
using Vime.Server.Features.Rooms.Services;
using Vime.Server.Features.Users.Services;
using Vime.Server.Infraestructure.Persistence;
using Vime.Server.Infraestructure.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{

    public static IServiceCollection AddFeaturesServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRoomFeatureServices(configuration);
        services.AddUsersFeatureServices(configuration);
        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddTransient<IDateTime, DateTimeService>();

        var databaseConecctionString = configuration.GetConnectionString("DatabaseConnection");
        services.AddDbContext<ApplicationDbContext>(dbContextOptions => dbContextOptions.UseSqlite(configuration["ConnectionStrings:DatabaseConnection"]));
        
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            var Key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            x.SaveToken = true;
            x.Events = new JwtBearerEvents();
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, // on production make it true
                ValidateAudience = false, // on production make it true
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            

        });
        return services;
    }

    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddControllers();

        services.AddSwaggerGen(setupAction =>
            {
                setupAction.EnableAnnotations();

                setupAction.AddSecurityDefinition("Vime", new OpenApiSecurityScheme() //Esto va a permitir usar swagger con el token.
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "Enter the Json Web Token:"
                });

                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Vime"
                        }
                    }, new List<string>()
                }
                });
            });

        
        services.AddSignalR();
        /*services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
        });
        */
        return services;
    }

}
