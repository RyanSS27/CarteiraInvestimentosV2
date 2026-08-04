using CarteiraInvestimentosAPI.Domain.Entities;
using CarteiraInvestimentosAPI.Entities;

namespace CarteiraInvestimentosAPI.Database;

public interface ITransactionRepository
{
    public Task AddTransactionAsync(Transaction transaction);
    public Task<List<Transaction>> ListTransactionsAsync(Guid customerId, int limit);
}