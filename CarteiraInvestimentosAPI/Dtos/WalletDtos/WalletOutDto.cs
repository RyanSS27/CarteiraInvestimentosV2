namespace CarteiraInvestimentosAPI.Dtos;

public record WalletOutDto(
    decimal totalValue,
    decimal totalValueUpToDate,
    decimal totalValueEstimated,
    DateTime calculationDate,
    List<AssetOutDto> assetsOut
        );