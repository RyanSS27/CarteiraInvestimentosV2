using CarteiraInvestimentosV2.Entities;

namespace CarteiraInvestimentosV2.Dtos;

public record CustomerOutResumeDto
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public bool IsActive { get; private set; }

    public CustomerOutResumeDto(Guid id, string name, bool isActive)
    {
        Id = id;
        Name = name;
        IsActive = isActive;
    }

    public CustomerOutResumeDto(Customer customer)
    {
        Id = customer.Id;
        Name = customer.Name;
        IsActive = customer.IsActive;
    }
}