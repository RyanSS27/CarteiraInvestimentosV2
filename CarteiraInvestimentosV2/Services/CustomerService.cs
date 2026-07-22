using CarteiraInvestimentosV2.Database;
using CarteiraInvestimentosV2.Dtos;
using CarteiraInvestimentosV2.Entities;
using MongoDB.Driver;

namespace CarteiraInvestimentosV2.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerCollection;

    public CustomerService(ICustomerRepository customerRepository)
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

    public async Task<CustomerOutDto?> UpdateCustomerInformation(Guid customerId, CustomerInputDto newCustomerData)
    {
        var customer = await _customerCollection.GetCustomerAsync(customerId);
        if (customer is null)
            return null;

        customer.Name = newCustomerData.Name;
        customer.Email = newCustomerData.Email;
        await _customerCollection.UpdateCustomerAsync(customer);
        return new CustomerOutDto(customer);
    }

    public async Task<CustomerOutResumeDto?> InactivateCustomer(Guid customerId)
    {
        var customer = await _customerCollection.GetCustomerAsync(customerId);
        if (customer is null)
            return null;
        
        customer.InactivateAccount();
        await _customerCollection.UpdateCustomerAsync(customer);
        return new CustomerOutResumeDto(customer);
    }

    public async Task<CustomerOutDto?> ActivateCustomer(Guid customerId)
    {
        throw new NotImplementedException();
    }

    // Funções utilizadas apenas para testes:
    public async Task<List<CustomerOutResumeDto>> ListCustomersAsync()
    {
        var customerResume = await _customerCollection.ListCustomerSummariesAsync();
        
        return customerResume
            .Select(c => new CustomerOutResumeDto(c.Id, c.Name, c.IsActive))
            .ToList();
    }

    public async Task<bool> DeleteCustomerAsync(Guid customerId)
    {
        return await _customerCollection.DeleteCustomerAsync(customerId);
    }
}