using System.ComponentModel.DataAnnotations;

namespace CarteiraInvestimentosV2.Dtos;

public class AssetInputDto
{
    [Required]
    public string Ticker { get; private set; }
    [Required]
    public int Quantity { get; private set; }
    [Required]
    public decimal AveragePrice { get; private set; }
}