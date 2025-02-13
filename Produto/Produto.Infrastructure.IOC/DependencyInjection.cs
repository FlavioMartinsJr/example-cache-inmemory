using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Produto.Domain.Interfaces;
using Produto.Infrastructure.Data.Contexts;
using Produto.Application.Interfaces;
using Produto.Application.Services;
using Produto.Infrastructure.Data.Repositories;

namespace Produto.Infrastructure.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, Settings configuration)
        {
            if (configuration.UseInMemoryDatabase)
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("Produto"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseNpgsql(configuration.ConnectionStrings.DefaultConnection,
                        x => x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                });
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            }

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<ApplicationDbContextInitializer>();

            return services;
        }
    }
}
