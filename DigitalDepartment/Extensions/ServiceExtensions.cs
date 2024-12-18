﻿using Contracts.RepositoryCore;
using Repository.Core;
using Service.Contracts;
using Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Entities.ConfigurationModels;
using Microsoft.OpenApi.Models;
using Entities.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using DigitalDepartment.Authorzation;
using Service.Contracts.DocsEntities;
using Service.DocsEntities;

namespace DigitalDepartment.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination"));
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
            });

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();
        public static void ConfigureDocumentVersionService(this IServiceCollection services) =>
            services.AddScoped<IDocumentVersionService, DocumentVersionService>();

        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        public static void ConfigureChecker(this IServiceCollection services) =>
            services.AddScoped<ICheckerService, CheckerService>();

        public static void ConfigureFilesFolders(this IServiceCollection services, IConfiguration configuration) 
        {
            var folder = configuration.GetValue<string>("BaseFolder");
            if (folder is null)
                throw new ArgumentNullException("No appsettings.json variable for default folder");
            services.AddSingleton<IFilesService>(new FilesService(folder));
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, Role>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 5;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
            
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration
        configuration)
        {
            var jwtConfiguration = new JwtConfiguration();
            configuration.Bind(jwtConfiguration.Section, jwtConfiguration);
            var envVar= Environment.GetEnvironmentVariable("SecretDigDep");
            var secretKey = envVar + envVar;
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfiguration.ValidIssuer,
                    ValidAudience = jwtConfiguration.ValidAudience,
                    IssuerSigningKey = new
                        SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }

        public static void AddJwtConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtConfiguration>("JwtSettings",
               configuration.GetSection("JwtSettings"));
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Digital Department API",
                    Version = "v1"
                });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                        },
                        new List<string>()
                    }
                });
            });
            
        }

        public static void ConfigureAuthorization(this IServiceCollection services)
        {            
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddAuthorization(opt =>
                {
                    opt.AddPolicy("ReadDocumentStatuses", 
                                  policy => policy.AddRequirements
                                  (
                                     new PermissionRequirement("ReadDocumentStatuses")
                                  ));
                    opt.AddPolicy("CreateDocumentStatus", 
                                  policy => policy.AddRequirements
                                  (
                                      new PermissionRequirement("CreateDocumentStatus")
                                  ));
                    opt.AddPolicy("Create",
                                 policy => policy.AddRequirements
                                 (
                                    new PermissionRequirement("Create")
                                 ));
                }
            );
        }

    }

}
