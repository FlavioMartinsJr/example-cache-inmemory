using Produto.API.Middlewares;
using Produto.Application.Mappings;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using Produto.Infrastructure.IOC;

namespace Produto.API.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebApiService(this IServiceCollection services, Settings configuration)
        {
            services.AddControllers();
            services.AddAutoMapper(typeof(DomainDTOMappingProfile));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddAuth(configuration.Identity);
            services.AddDistributedMemoryCache();
            services.AddMemoryCache();

            // Middleware
            services.AddScoped<ExceptionMiddleware>();
            services.AddScoped<PerformanceMiddleware>();
            services.AddScoped<Stopwatch>();

            // Extension classes
            services.AddCompressionCustom();
            services.AddHttpClient();
            services.AddCorsCustom(configuration);
            services.AddSwaggerOpenAPI(configuration);

            // Json configuration
            services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.WriteIndented = true;
                options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            return services;
        }
    }
}
