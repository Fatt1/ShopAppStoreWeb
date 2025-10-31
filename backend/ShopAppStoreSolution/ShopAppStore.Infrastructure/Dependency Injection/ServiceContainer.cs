using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopAppStore.Domain.Interfaces.Logging;
using ShopAppStore.Domain.Interfaces.Repositories;
using ShopAppStore.Infrastructure.Entities;
using ShopAppStore.Infrastructure.Repositories;
using ShopAppStore.Infrastructure.Services.Logging;

namespace ShopAppStore.Infrastructure.Dependency_Injection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastureService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Connection_String"];

            #region Configure DbContext and Identity
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(ServiceContainer).Assembly.FullName);
                });

            });

            //THÊM ĐOẠN NÀY ĐỂ SỬ DỤNG IDENTITY
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;

            }).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.AddDataProtection();
            #endregion


            #region Configure Repositories
            services.AddScoped<IAppRepository, AppRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Configure Logging
            services.AddScoped(typeof(IAppLogger<>), typeof(SerilogLoggerAdapter<>));
            #endregion

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceContainer).Assembly));
            return services;
        }
    }
}

