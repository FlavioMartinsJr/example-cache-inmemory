using Produto.API.Middlewares;
using Produto.Infrastructure.Data.Contexts;
using Produto.Infrastructure.IOC;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using Serilog.Core;

namespace Produto.API.Extensions
{
    public static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder, Settings appsettings)
        {
            builder.Host.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .WriteTo.File(
                    path:"logs/log-"+ DateTime.Now.ToString("yyyy-MM-dd") +".txt", 
                    rollingInterval: RollingInterval.Infinite, 
                    retainedFileCountLimit: 1,
                    fileSizeLimitBytes: 10485760,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}"
                )
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}"
                )
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId());
            builder.Services.AddMvc();
            builder.Services.AddInfrastructureServices(appsettings);
            builder.Services.AddWebApiService(appsettings);
            return builder.Build();
        }

        public async  static Task<WebApplication> ConfigurePipeline(this WebApplication app, Settings appsettings)
        {
            using var loggerFactory = LoggerFactory.Create(builder => { });
            using var scope = app.Services.CreateScope();

            if (!appsettings.UseInMemoryDatabase)
            {
                var initialize = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
                await initialize.InitializeAsync();
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<LoggingMiddleware>();
            app.UseMiddleware<PerformanceMiddleware>();
            app.ConfigureExceptionHandler(loggerFactory.CreateLogger("Exceptions"));
            app.UseSerilogRequestLogging();
            app.UseCors("PolicyCors");
            app.UseHttpsRedirection();
            app.UseSwagger(appsettings);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSerilogRequestLogging();
            app.MapControllers();

            return app;
        }
    }
}
