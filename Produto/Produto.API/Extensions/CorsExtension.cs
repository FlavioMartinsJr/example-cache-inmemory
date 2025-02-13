using Produto.Infrastructure.IOC;

namespace Produto.API.Extensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCorsCustom(this IServiceCollection services, Settings appSettings)
        {
            return services.AddCors(options => options.AddPolicy("PolicyCors",
                 policy => policy
                     /*.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins(appSettings.Cors)); */
                     .AllowAnyOrigin()
                     .AllowAnyHeader()
                     .AllowAnyMethod()));
        }
    }
}
