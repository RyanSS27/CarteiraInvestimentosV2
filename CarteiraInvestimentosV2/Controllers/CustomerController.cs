using CarteiraInvestimentosV2.Dtos;
using CarteiraInvestimentosV2.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraInvestimentosV2.Controllers;

[Route("api/customer")]
[ApiController]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
    private readonly ICustomerService _customerService = customerService;
    
    [HttpPost]
    public async Task<IActionResult> AddCustomer(CustomerInputDto customer)
    {
        var newCustomer = await _customerService.AddCustomerAsync(customer);
        return CreatedAtAction(nameof(GetCustomer), new {id = newCustomer.Id}, newCustomer);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCustomer(Guid id)
    {
        var customerOutDto = await _customerService.GetCustomer(id);
        if (customerOutDto is null)
            return NotFound(new { mensagem = $"Cliente de id '{id}' não encontrado." });

        return Ok(customerOutDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCustomer(Guid id, CustomerInputDto newCustomerData)
    {
        var customer = await _customerService.UpdateCustomerInformation(id, newCustomerData);
        if (customer is null)
            return NotFound(new { mensagem = $"Cliente de id '{id}' não encontrado." });

        return Ok(customer);
    }

    [HttpPut("inactivate/{id:guid}")]
    public async Task<IActionResult> InactivateCustomer(Guid id)
    {
        var inactiveCustomer = await _customerService.InactivateCustomer(id);
        if (inactiveCustomer is null)
            return NotFound(new { mensagem = $"Cliente de id '{id}' não encontrado." });

        return Ok(inactiveCustomer);
    }

    [HttpPut("activate/{id:guid}")]
    public async Task<IActionResult> ActivateCustomer(Guid id)
    {
        var activeCustomer = await _customerService.ActivateCustomer(id);
        if (activeCustomer is null)
            return NotFound(new { mensagem = $"Cliente de id '{id}' não encontrado." });

        return Ok(activeCustomer);
    }
    
    // Chamadas utilizadas para testes e debug:

    [HttpGet]
    public async Task<IActionResult> ListCustomers()
    {
        List<CustomerOutResumeDto> customers = await _customerService.ListCustomersAsync();
        return Ok(customers);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        var result = await _customerService.DeleteCustomerAsync(id);
        if (result)
            return NoContent();

        return NotFound(new { mensagem = $"Cliente de id '{id}' não encontrado." });
    }
}