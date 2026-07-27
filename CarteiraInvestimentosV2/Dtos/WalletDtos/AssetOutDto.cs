namespace CarteiraInvestimentosV2.Dtos;

public record AssetOutDto(
    string Ticker,
    int CurrentQuantity,
    decimal AveragePrice,
    decimal CurrentAmountInvested,
    decimal CurrentMarketPrice,
    decimal TotalCurrentValue,
    decimal ReturnPercentage,
    decimal ProfitOrLoss,
    bool IsPriceUpToDate
    );