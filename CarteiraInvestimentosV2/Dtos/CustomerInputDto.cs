using System.ComponentModel.DataAnnotations;

namespace CarteiraInvestimentosV2.Dtos;

public class CustomerInputDto
{
    [Required]
    public string Name { get; set; }
    public string Email { get; set; } 
}