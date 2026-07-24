using CarteiraInvestimentosV2.Entities;
using CarteiraInvestimentosV2.Entities.Enums;
using MongoDB.Driver;

namespace CarteiraInvestimentosV2.Database;

public class TransactionRepository : ITransactionRepository
{
    private readonly IMongoCollection<Transaction> _transactionCollection;

    public TransactionRepository(IMongoClient mongoClient, IConfiguration configuration)
    {
        var databaseName = configuration.GetValue<string>("CarteiraInvestimentosAPI:DatabaseName");
        var collectionName = configuration.GetValue<string>("CarteiraInvestimentosAPI:TransactionsCollectionName");

        var database = mongoClient.GetDatabase(databaseName);
        _transactionCollection = database.GetCollection<Transaction>(collectionName);
    }

    public async Task AddTransactionAsync(Transaction transaction)
    {
        await _transactionCollection.InsertOneAsync(transaction);
    }

    // a ideia é, futuramente listar as transações de um cliente para exibir no front, por exemplo
    public async Task<List<Transaction>> ListTransactionsAsync(Guid customerId, int limit)
    {
        throw new NotImplementedException();
    }
}