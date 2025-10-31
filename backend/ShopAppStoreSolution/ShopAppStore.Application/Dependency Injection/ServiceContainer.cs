using Microsoft.Extensions.DependencyInjection;
using ShopAppStore.Application.Services.Implements;
using ShopAppStore.Application.Services.Interfaces;

namespace ShopAppStore.Application.Dependency_Injection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceContainer).Assembly));

            #region Add Denpendency
            services.AddScoped<ISlugGenerator, SlugGenerator>();
            #endregion
            return services;
        }
    }
}
