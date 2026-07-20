using CarteiraInvestimentosV2.Entities;

namespace CarteiraInvestimentosV2.Dtos;

public record CustomerOutResumeDto
{
    public Guid CustomerId { get; private set; }
    public string Name { get; private set; }

    public CustomerOutResumeDto(Guid customerId, string name)
    {
        CustomerId = customerId;
        Name = name;
    }

    public CustomerOutResumeDto(Customer customer)
    {
        CustomerId = customer.Id;
        Name = customer.Name;
    }
}