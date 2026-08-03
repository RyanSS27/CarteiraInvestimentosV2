namespace CarteiraInvestimentosV2.Dtos;

public record WalletOutDto(
    decimal totalValue,
    decimal totalValueUpToDate,
    decimal totalValueEstimated,
    DateTime calculationDate,
    List<AssetOutDto> assetsOut
        );