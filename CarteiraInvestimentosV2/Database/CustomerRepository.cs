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

    public Task<List<Customer>> ListCustomersAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddCustomerAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> GetCustomerAsync(Guid customerId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCustomerAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCustomerAsync(Guid customerId)
    {
        throw new NotImplementedException();
    }
}