using DataAccess.MongoDB.Repositories;
using Domain.Jwt;
using Domain.Repositories;
using Domain.Services;
using Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System.Reflection;
using System.Text;

namespace API.Abstract
{
    public static class ProgramExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection service)
        {
            service.AddScoped(provider =>
            {
                var settings = provider.GetRequiredService<IOptions<PolizaDatabaseSettings>>();
                var client = new MongoClient(settings.Value.ConnectionString);
                var mongoDatabase = client.GetDatabase(settings.Value.DatabaseName);

                return mongoDatabase;
            });
            service.AddScoped<IPolizaService, PolizaService>();
            service.AddScoped<IPolizaRepository, PolizaRepository>();
            service.AddScoped<IUsuarioRepository, UsuarioRepository>();
            service.AddScoped<IJwtFactory, JwtFactory>();

            return service;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "An ASP.NET Core Web API for managing ToDo items",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });

                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            // Add Authentication
            services.Configure<JwtSettings>(
                configuration.GetSection(nameof(JwtSettings))
            );
            var jwt = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
            var secretKey = Encoding.ASCII.GetBytes(jwt.Key);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}
