namespace ScoutVenture.CoreContracts
{
    public interface ITransactionManager
    {
        Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default);
    }
}