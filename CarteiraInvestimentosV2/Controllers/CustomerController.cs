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
            return NotFound(new { mensagem = "Cliente não encontrado"});

        return Ok(customerOutDto);
    }
}