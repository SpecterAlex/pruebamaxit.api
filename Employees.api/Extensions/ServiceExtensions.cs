using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Employees.Api.Services;
using Employees.Business.Interfaces;
using Employees.Business.Repositories;
using Employees.Data;
using Employees.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Employees.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {

                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });

            });
        }

        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });
        }

        public static void ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IEmployeesRepository, EmployeesRepository>();
            services.AddScoped<ICountriesRepository, CountriesRepository>();
            services.AddScoped<IBeneficiariesRepository, BeneficiariesRepository>();
            

            // Capa de acceso a datos
            services.AddScoped<UsersData>();
            services.AddScoped<EmployeesData>();
            services.AddScoped<CountriesData>();
            services.AddScoped<BeneficiariesData>();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddSingleton<TokenService>();
        }
    }
}
