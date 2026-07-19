using CarteiraInvestimentosV2.Dtos;
using CarteiraInvestimentosV2.Entities;

namespace CarteiraInvestimentosV2.Database;

public interface ICustomerRepository
{
    public Task<List<Customer>> ListCustomersAsync();
    public Task AddCustomerAsync(Customer customer);
    public Task<Customer?> GetCustomerAsync(Guid customerId);
    public Task UpdateCustomerAsync(Customer customer);
    public Task DeleteCustomerAsync(Guid customerId);
}