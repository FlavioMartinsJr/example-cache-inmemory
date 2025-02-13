using Produto.Domain.Interfaces;
using Produto.Infrastructure.Data.Contexts;
using Produto.Application.Exceptions;

namespace Produto.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IProdutoRepository ProdutoRepository { get; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            ProdutoRepository = new ProdutoRepository(_context);
        }

        public async Task SaveChangesAsync(CancellationToken token)
        {
            await _context.SaveChangesAsync(token);
        }

        public async Task ExecuteTransactionAsync(Action action, CancellationToken token)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(token);
            try
            {
                action();
                await _context.SaveChangesAsync(token);
                await transaction.CommitAsync(token);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(token);
                throw TransactionException.TransactionNotExecuteException(ex);
            }
        }

        public async Task ExecuteTransactionAsync(Func<Task> action, CancellationToken token)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(token);
            try
            {
                await action();
                await _context.SaveChangesAsync(token);
                await transaction.CommitAsync(token);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(token);
                throw TransactionException.TransactionNotExecuteException(ex);
            }
        }
    }
}
