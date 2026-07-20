using CarteiraInvestimentosV2.Database;
using CarteiraInvestimentosV2.Dtos;
using CarteiraInvestimentosV2.Entities;
using MongoDB.Driver;

namespace CarteiraInvestimentosV2.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerCollection;

    private CustomerService(ICustomerRepository customerRepository)
    {
        _customerCollection = customerRepository;
    }
    
    
    
    public async Task<CustomerOutDto> AddCustomerAsync(CustomerInputDto newCustomer)
    {
        var customer = new Customer(newCustomer.Name, newCustomer.Email);
        await _customerCollection.AddCustomerAsync(customer);

        return new CustomerOutDto(customer);
    }

    public async Task<CustomerOutDto?> GetCustomer(Guid customerId)
    {
        var customer = await _customerCollection.GetCustomerAsync(customerId);
        if (customer is null)
            return null;
        
        return new CustomerOutDto(customer);
    }

    public Task<CustomerOutDto> UpdateCustomerInformation(Guid customerId, CustomerInputDto customerInformation)
    {
        throw new NotImplementedException();
    }

    public Task<CustomerOutResumeDto> InactiveCustomer(Guid customerId)
    {
        throw new NotImplementedException();
    }

    public Task<CustomerOutDto> ActiveCustomer(Guid customerId)
    {
        throw new NotImplementedException();
    }

    public Task<List<CustomerOutResumeDto>> ListCustomersAsync()
    {
        throw new NotImplementedException();
    }
}