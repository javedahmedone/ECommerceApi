using ECommerce.Business.Interfaces;
using ECommerce.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
