using CarteiraInvestimentosV2.Entities;

namespace CarteiraInvestimentosV2.Dtos;

public record CustomerOutDto
{
    public Guid id { get; set; }
    public string nome { get; set; }
    public string email { get; set; }

    public CustomerOutDto() {}
    
    public CustomerOutDto(Customer customer)
    {
            id = customer.Id;
            nome = customer.Name;
            email = customer.Email;
    }
}