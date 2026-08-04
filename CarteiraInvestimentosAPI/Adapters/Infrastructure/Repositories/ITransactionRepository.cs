using CarteiraInvestimentosAPI.Domain.Entities;
using CarteiraInvestimentosAPI.Entities;
using CarteiraInvestimentosAPI.Entities.Enums;

namespace CarteiraInvestimentosAPI.Database;

public interface ITransactionRepository
{
    public Task AddTransactionAsync(Transaction transaction);
    public Task<List<Transaction>> ListTransactionsAsync(Guid customerId, int limit);
}