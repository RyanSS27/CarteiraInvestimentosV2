using System.Text.Json;
using System.Text.Json.Serialization;
using Flurl;
using Flurl.Http;

namespace CarteiraInvestimentosV2.Adapters.Infrastructure.ExternalServices;

public record BrapiResponse(
    [property: JsonPropertyName("results")] List<BrapiResult>? Results
);

public record BrapiResult(
    [property: JsonPropertyName("symbol")] string Symbol, 
    [property: JsonPropertyName("data")] BrapiData? Data
);

public record BrapiData(
    [property: JsonPropertyName("regularMarketPrice")] decimal? RegularMarketPrice
);

public class BrapiService(IConfiguration configuration) : IFinancialMarketService
{
    private readonly string _baseUrl = configuration["Brapi:BaseUrl"] 
                                       ?? throw new ArgumentNullException($"BaseUrl da Brapi não configurada em appsettings.");
    
    private readonly string _token = configuration["Brapi:Token"]
                                     ?? throw new ArgumentNullException($"Token da API da Brapi não configurado em appsettings.");

    public async Task<Dictionary<string, decimal>> GetPriceAsync(List<string> tickers)
    {
        var marketPrices = new Dictionary<string, decimal>();

        if (tickers.Count == 0) return marketPrices;

        foreach (var ticker in tickers)
        {
            try
            {
                // Busca o texto puro na API
                var rawJson = await _baseUrl
                    .SetQueryParams(new { symbols = ticker, token = _token })
                    .GetStringAsync();

                // Converte para C#
                var response = JsonSerializer.Deserialize<BrapiResponse>(rawJson);
                var asset = response?.Results?.FirstOrDefault();

                if (asset != null && asset.Data?.RegularMarketPrice > 0) {
                    marketPrices[asset.Symbol] = asset.Data.RegularMarketPrice.Value;
                /*
                    O .Value é necessário porque, mesmo que não seja possível que um nulo passe
                    devido à condição, o compilador entende que talvez passe e pede um decimal.
                    Nos "bastidores", esse null é um Nullable<decimal> e o .Value faz com que ele
                    desempacote e extraia um decimal que está dentro.
                */
                } 
                else
                {
                    // Cai aqui se a Brapi não achar a ação ou se o preço vier nulo/zero
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n[AVISO] O ativo '{ticker}' não retornou um preço válido na Brapi. O sistema utilizará o preço médio.");
                    Console.WriteLine($"[JSON RECEBIDO]: {rawJson}");
                    Console.ResetColor(); 
                }
            }
            catch (FlurlHttpException ex)
            {
                // Erros de servidor da Brapi (Ex: 401 Não Autorizado, 500 Erro Interno)
                var errorBody = await ex.GetResponseStringAsync();
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[ERRO HTTP] A Brapi recusou a requisição para '{ticker}'. Status: {ex.StatusCode}");
                Console.WriteLine($"[DETALHES DA RECUSA]: {errorBody}");
                Console.ResetColor();
            } 
            catch (Exception ex)
            {
                // Erros locais (Ex: Sem internet, falha de memória)
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\n[ERRO FATAL] Falha interna da aplicação ao processar '{ticker}': {ex.Message}");
                Console.ResetColor();
            }
        }

        return marketPrices;
    }
}