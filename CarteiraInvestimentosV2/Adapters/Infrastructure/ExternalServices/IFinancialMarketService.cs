namespace CarteiraInvestimentosV2.Adapters.Infrastructure.ExternalServices;

public interface IFinancialMarketService
{
    Task<Dictionary<string, decimal>> GetPriceAsync(List<string> ticker);
}