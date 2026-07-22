using CarteiraInvestimentosV2.Dtos;

namespace CarteiraInvestimentosV2.Services;

public interface ICustomerService
{
    public Task<CustomerOutDto> AddCustomerAsync(CustomerInputDto newCustomer);
    public Task<CustomerOutDto?> GetCustomer(Guid customerId);
    public Task<CustomerOutDto> UpdateCustomerInformation(Guid customerId, CustomerInputDto customerInformation);
    
    public Task<CustomerOutResumeDto> InactiveCustomer(Guid customerId);
    public Task<CustomerOutDto> ActiveCustomer(Guid customerId);

    // Funções utilizadas apenas para testes:
    public Task<List<CustomerOutResumeDto>> ListCustomersAsync();
    public Task<bool> DeleteCustomerAsync(Guid customerID);
}
