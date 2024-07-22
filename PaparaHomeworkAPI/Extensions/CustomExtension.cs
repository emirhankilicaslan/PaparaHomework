using PaparaHomeworkAPI.Services;

namespace PaparaHomeworkAPI.Extensions
{
    public static class CustomExtension
    {
        public static void ServiceExtension(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddControllers().AddNewtonsoftJson();
            services.AddScoped<ILoginService, LoginService>();
        }
    }
}