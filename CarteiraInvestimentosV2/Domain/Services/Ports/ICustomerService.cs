using CarteiraInvestimentosV2.Dtos;
using CarteiraInvestimentosV2.Dtos.CustomersDtos;

namespace CarteiraInvestimentosV2.Domain.Services.Ports;

public interface ICustomerService
{
    public Task<CustomerOutDto> AddCustomerAsync(CustomerInputDto newCustomer);
    public Task<CustomerOutDto?> GetCustomer(Guid customerId);
    public Task<CustomerOutDto?> UpdateCustomerInformation(Guid customerId, CustomerInputDto newCustomerData);
    
    public Task<CustomerOutResumeDto?> InactivateCustomer(Guid customerId);
    public Task<CustomerOutDto?> ActivateCustomer(Guid customerId);

    // Funções utilizadas apenas para testes:
    public Task<List<CustomerOutResumeDto>> ListCustomersAsync();
    public Task<bool> DeleteCustomerAsync(Guid customerId);
}
