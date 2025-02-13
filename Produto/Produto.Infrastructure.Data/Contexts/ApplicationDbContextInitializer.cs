using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Produto.Domain.Entities;

namespace Produto.Infrastructure.Data.Contexts
{
    public class ApplicationDbContextInitializer(ApplicationDbContext context, ILoggerFactory logger)
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger _logger = logger.CreateLogger<ApplicationDbContextInitializer>();

        public async Task InitializeAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
                await CreatedProdutos();
            }
            catch (Exception exception)
            {
                _logger.LogError("Migration error {exception}", exception);
                throw;
            }
        }

        public async Task CreatedProdutos()
        {
            if (await _context.Produto.AnyAsync()) return;

            var faker = new Faker<TblProduto>()
            .RuleFor(p => p.Titulo, f => f.Commerce.ProductName())
            .RuleFor(p => p.Valor, f => f.Random.Decimal(1, 1000))
            .RuleFor(p => p.DataCriado, f => f.Date.Past(2))
            .RuleFor(p => p.DataAlterado, f => f.Random.Bool() ? f.Date.Recent(30) : null);
            
            int totalRegistros = 1000000;
            int batchSize = 200000; 

            for (int i = 0; i < totalRegistros / batchSize; i++)
            {
                var produtos = faker.Generate(batchSize);
                await _context.Produto.AddRangeAsync(produtos);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Inseridos {((i + 1) * batchSize)} registros...");
            }

            _logger.LogInformation("População do banco finalizada!");
        }
    }
}
