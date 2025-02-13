namespace Produto.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IProdutoRepository ProdutoRepository { get; }
        Task SaveChangesAsync(CancellationToken token);
        Task ExecuteTransactionAsync(Action action, CancellationToken token);
        Task ExecuteTransactionAsync(Func<Task> action, CancellationToken token);
    }
}