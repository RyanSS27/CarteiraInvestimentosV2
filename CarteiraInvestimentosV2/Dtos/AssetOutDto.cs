using CarteiraInvestimentosV2.Entities;

namespace CarteiraInvestimentosV2.Dtos;

public record AssetOutDto
{
    public string Ticker { get; private set; }
    public int Quantity { get; private set; }
    public decimal AveragePrice { get; private set; }
    public decimal TotalInvestedValue { get; private set; }

    public AssetOutDto(Asset asset) 
    {
        Ticker = asset.Ticker;
        Quantity = asset.Quantity;
        AveragePrice = asset.AveragePrice;
        TotalInvestedValue = asset.TotalInvestedValue;
    }
}