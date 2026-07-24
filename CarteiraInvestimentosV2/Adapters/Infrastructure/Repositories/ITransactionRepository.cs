using CarteiraInvestimentosV2.Entities;
using CarteiraInvestimentosV2.Entities.Enums;

namespace CarteiraInvestimentosV2.Database;

public interface ITransactionRepository
{
    public Task AddTransactionAsync(Transaction transaction);
    public Task<List<Transaction>> ListTransactionsAsync(Guid customerId, int limit);
}