using CarteiraInvestimentosV2.Domain.Entities;

namespace CarteiraInvestimentosV2.Dtos.CustomersDtos;

public record CustomerOutDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    
    public CustomerOutDto(Customer customer)
    {
            Id = customer.Id;
            Name = customer.Name;
            Email = customer.Email;
    }
}