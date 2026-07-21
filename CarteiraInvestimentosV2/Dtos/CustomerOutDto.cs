using CarteiraInvestimentosV2.Entities;

namespace CarteiraInvestimentosV2.Dtos;

public record CustomerOutDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    
    // adicionar a lista de ativos depois
    
    public List<Transaction> Transactions { get; private set; }

    public CustomerOutDto() {}
    
    public CustomerOutDto(Customer customer)
    {
            Id = customer.Id;
            Name = customer.Name;
            Email = customer.Email;
        
    }
}