namespace CarteiraInvestimentosAPI.Adapters.Infrastructure.ExternalServices;

public interface IFinancialMarketService
{
    Task<Dictionary<string, decimal>> GetPriceAsync(List<string> ticker);
}