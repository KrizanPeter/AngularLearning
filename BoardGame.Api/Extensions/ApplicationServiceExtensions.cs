using System;

using API.DtoMappers;
using API.Entities.Context;

using BoardGame.Domain.ModelMapperProfile;
using BoardGame.Domain.Repositories;
using BoardGame.Domain.Repositories.Interfaces;
using BoardGame.Service.Services.AuthServices;
using BoardGame.Services.Services;
using BoardGame.Services.Services.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IHeroRepository, HeroRepository>();
            services.AddScoped<IHeroService, HeroService>();
            services.AddScoped<IBlockRepository, BlockRepository>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<IChatMessageService, ChatMessageService>();
            services.AddScoped<IBlockService, BlockService>();
            services.AddScoped<IBlockTypeRepository, BlockTypeRepository>();
            services.AddScoped<IMonsterRepository, MonsterRepository>();
            services.AddScoped<IMonsterService, MonsterService>();
            services.AddScoped<IMonsterTypeRepository, MonsterTypeRepository>();


            services.AddAutoMapper(typeof(DtoMapperProfile).Assembly);
            services.AddAutoMapper(typeof(ModelMapperProfile).Assembly);
            services.AddDbContext<DataContext>(options =>
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                string connStr;

                if (env == "DevelopmentMsql")
                {
                    connStr = config.GetConnectionString("DefaultConnection");
                    options.UseSqlServer(connStr);
                }
                else
                {
                    if (env == "Development")
                    {
                        connStr ="Server=localhost;Port=5432;User Id=postgres;Password=admin;Database=BoardGameDb";
                    }
                    else
                    {
                        var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
                        // Parse connection URL to connection string for Npgsql
                        connUrl = connUrl.Replace("postgres://", string.Empty);
                        var pgUserPass = connUrl.Split("@")[0];
                        var pgHostPortDb = connUrl.Split("@")[1];
                        var pgHostPort = pgHostPortDb.Split("/")[0];
                        var pgDb = pgHostPortDb.Split("/")[1];
                        var pgUser = pgUserPass.Split(":")[0];
                        var pgPass = pgUserPass.Split(":")[1];
                        var pgHost = pgHostPort.Split(":")[0];
                        var pgPort = pgHostPort.Split(":")[1];

                        connStr = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Require;TrustServerCertificate=True";
                    }
                    options.UseNpgsql(connStr);
                }
            });

            return services;
        }
    }
}
