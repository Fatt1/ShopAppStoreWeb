using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopAppStore.Application.Services.Interfaces;
using ShopAppStore.Domain.Interfaces.Logging;
using ShopAppStore.Domain.Interfaces.Repositories;
using ShopAppStore.Infrastructure.Entities;
using ShopAppStore.Infrastructure.Repositories;
using ShopAppStore.Infrastructure.Services.Encryption;
using ShopAppStore.Infrastructure.Services.Logging;
using ShopAppStore.Infrastructure.Services.UploadImage;

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
            services.AddScoped<IDurationRepository, DurationRepository>();
            services.AddScoped<IPlatFormRepository, PlatFormRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IComboRepository, ComboRepository>();
            services.AddScoped<IComboTypeRepository, ComboTypeRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Configure Logging
            services.AddScoped(typeof(IAppLogger<>), typeof(SerilogLoggerAdapter<>));
            #endregion


            #region Add Denpendency Services
            services.AddScoped<IImageUploadService, CloudinaryService>();
            services.AddScoped<IEncryptionService, AesEncryptionService>();
            #endregion

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceContainer).Assembly));


            return services;
        }
    }
}

