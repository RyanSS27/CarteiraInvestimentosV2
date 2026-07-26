namespace CarteiraInvestimentosV2.Dtos;

public record AssetOutDto(
    string Ticker,
    int CurrentQuantity,
    decimal AveragePrice,
    decimal CurrentMarketPrice,
    decimal TotalInvestedValue,
    decimal TotalCurrentValue,
    decimal ReturnPercentage,
    decimal ProfitOrLoss,
    bool IsPriceUpToDate
    );