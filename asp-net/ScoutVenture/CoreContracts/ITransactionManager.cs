namespace ScoutVenture.CoreContracts
{
    public interface ITransactionManager
    {
        Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken);
        Task<T> ExecuteAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken);
    }
}