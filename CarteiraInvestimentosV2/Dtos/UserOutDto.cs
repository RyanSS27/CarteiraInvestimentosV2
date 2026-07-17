using CarteiraInvestimentosV2.Entities;

namespace CarteiraInvestimentosV2.Dtos;

public record UserOutDto
{
    public Guid id { get; set; }
    public string nome { get; set; }
    public string email { get; set; }

    public UserOutDto() {}
    
    public UserOutDto(Customer customer)
    {
            id = customer.Id;
            nome = customer.Name;
            email = customer.Email;
    }
}