using System.Data.Common;
using System.Runtime.InteropServices.Marshalling;
using CarteiraInvestimentosV2.Entities;
using MongoDB.Driver;

namespace CarteiraInvestimentosV2.Database;

public class CustomerRepository : ICustomerRepository
{
    public readonly IMongoCollection<Customer> _customersCollection;

    public CustomerRepository(IMongoClient mongoClient, IConfiguration configuration)
    {
        var databaseName = configuration.GetValue<string>("CarteiraInvestimentosAPI:DatabaseName");
        var collectionName = configuration.GetValue<string>("CarteiraInvestimentosAPI:CustomerCollectionName");

        var database = mongoClient.GetDatabase(databaseName);

        _customersCollection = database.GetCollection<Customer>(collectionName);
    }

    public async Task<List<CustomerSummary>> ListCustomerSummariesAsync()
    {
        return await _customersCollection
            .Find(_ => true) // Retorna todos
            .SortByDescending(c => c.IsActive) 
            .Project(c => new CustomerSummary(c.Id, c.Name, c.IsActive)) // Limita os dados tragos 
            .ToListAsync();
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        await _customersCollection.InsertOneAsync(customer);
    }

    public async Task<Customer?> GetCustomerAsync(Guid customerId)
    {
        return await _customersCollection.Find(c => c.Id == customerId).FirstOrDefaultAsync();
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        await _customersCollection.ReplaceOneAsync(c => c.Id == customer.Id, customer);
    }
}