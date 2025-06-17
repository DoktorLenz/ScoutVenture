using Microsoft.EntityFrameworkCore.Storage;
using ScoutVenture.CoreContracts;

namespace ScoutVenture.PostgresAdapter
{
    public class TransactionManager(PostgresApplicationDbContext dbContext) : ITransactionManager
    {
        public async Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default)
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                await action();

                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}