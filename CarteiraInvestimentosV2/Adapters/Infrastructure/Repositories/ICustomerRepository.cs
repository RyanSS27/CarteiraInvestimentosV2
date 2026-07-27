using CarteiraInvestimentosV2.Domain.Entities;
using CarteiraInvestimentosV2.Entities;

namespace CarteiraInvestimentosV2.Adapters.Infrastructure.Repositories;

public interface ICustomerRepository
{
    public Task AddCustomerAsync(Customer customer);
    public Task<Customer?> GetCustomerAsync(Guid customerId);
    public Task UpdateCustomerAsync(Customer customer);

    // Funções utilizadas apenas para testes:
    public Task<List<CustomerSummary>> ListCustomerSummariesAsync();
    public Task<bool> DeleteCustomerAsync(Guid customerId);
}