using CarteiraInvestimentosV2.Adapters.Infrastructure.ExternalServices;
using CarteiraInvestimentosV2.Adapters.Infrastructure.Repositories;
using CarteiraInvestimentosV2.Database;
using CarteiraInvestimentosV2.Domain.Entities;
using CarteiraInvestimentosV2.Domain.Exceptions;
using CarteiraInvestimentosV2.Domain.Services.Ports;
using CarteiraInvestimentosV2.Dtos;
using CarteiraInvestimentosV2.Entities.Enums;
using Asset = CarteiraInvestimentosV2.Domain.Entities.Asset;

namespace CarteiraInvestimentosV2.Domain.Services;

public class WalletService(ICustomerRepository customerRepository, ITransactionRepository transactionRepository)
    : IWalletService
{
    private const int LimitPerRequest = 25;

    public async Task<WalletOutDto> GetWalletSummary(Guid customerId)
    {
        var customer = await customerRepository.GetCustomerAsync(customerId);
        if (customer is null)
            throw new NotFoundException($"Cliente de id '{customerId}' não encontrado");

        if (customer.Assets.Count <= 0)
            throw new DomainException($"Cliente {customer.Name} de id {customerId} não possui investimentos em ativos no momento."); // odiei essa frase kkkkkkkkk

        decimal totalValue = 0;
        decimal totalValueUpToDate = 0;
        decimal totalValueEstimated = 0;
         
        List<AssetOutDto> assetsOut = [];
        
        foreach (var asset in customer.Assets)
        {
            decimal currentMarketPrice = asset.AveragePrice; 
            bool isPriceUpToDate = false;
            
            try
            {
                throw new NotImplementedException();
                isPriceUpToDate = true;
            }
            catch
            {
                // ignored
            }
            
            /*
                Nota pessoal: recebe o valor total atualizado com os valores da Brapi.
                Caso não receba, acaba recebendo o valor que total investido pelo cliente,
                visto que o próprio asset faz o mesmo cálculo internamente
                CurrentAmountInvested = quantity * average price (que está como padrão em currentMarketPrice) 
            */
            decimal totalCurrentValue = asset.Quantity * currentMarketPrice; 
            
            decimal profitOrLoss = totalCurrentValue - asset.CurrentAmountInvested;
            
            decimal returnPercentage = asset.AveragePrice > 0 ? 
                ((currentMarketPrice / asset.AveragePrice) - 1) * 100 
                : 0;

            assetsOut.Add(new AssetOutDto(
                asset.Ticker,
                asset.Quantity,
                asset.AveragePrice,
                asset.CurrentAmountInvested,
                currentMarketPrice,
                totalCurrentValue,
                Math.Round(returnPercentage, 2),
                Math.Round(profitOrLoss, 2),
                isPriceUpToDate
            ));
            
            totalValue += totalCurrentValue;
            
            if (isPriceUpToDate)
                totalValueUpToDate += totalCurrentValue;
            else
                totalValueEstimated += totalCurrentValue;
        }
        
        return new WalletOutDto(
            totalValue,
            totalValueUpToDate,
            totalValueEstimated,
            DateTime.UtcNow,
            assetsOut);
    }

    public async Task<List<TransactionOutDto>> ListCustomerTransactionAsync(Guid customerId, int limit)
    {
        var customer = await customerRepository.GetCustomerAsync(customerId);
        if (customer is null)
            throw new NotFoundException($"Cliente de id '{customerId}' não encontrado");
        
        if (limit > LimitPerRequest || limit <= 0)
            limit = LimitPerRequest;
        
        var transaction = await transactionRepository.ListTransactionsAsync(customerId, limit);
        return transaction
            .Select(MapToTransactionOutDto) // versão implicita que passa cada transação para a função
            .ToList();
    }

    public async Task<TransactionOutDto> RecordTransactionAsync(Guid customerId, TransactionInputDto transactionInput)
    {
        // diversão começa aqui
        var customer = await customerRepository.GetCustomerAsync(customerId);
        if (customer is null)
            throw new NotFoundException($"Cliente de id '{customerId}' não encontrado");
        
        switch (transactionInput.TransactionType)
        {
            case TransactionType.BUY:
            {
                customer.AddAsset(new Asset(
                    transactionInput.Ticker,
                    transactionInput.Quantity,
                    transactionInput.UnitPrice
                ));
            
                var transaction = new Transaction(
                    customerId,
                    transactionInput.TransactionType,
                    transactionInput.Quantity,
                    transactionInput.UnitPrice,
                    transactionInput.Ticker);
                
                await customerRepository.UpdateCustomerAsync(customer);
                await transactionRepository.AddTransactionAsync(transaction);
            
                return MapToTransactionOutDto(transaction);
            }
            case TransactionType.SELL:
            {
                customer.SellAsset(transactionInput.Ticker, transactionInput.Quantity);

                var transaction = new Transaction(
                    customerId,
                    transactionInput.TransactionType,
                    transactionInput.Quantity,
                    transactionInput.UnitPrice,
                    transactionInput.Ticker
                    );

                await customerRepository.UpdateCustomerAsync(customer);
                await transactionRepository.AddTransactionAsync(transaction);
                
                return MapToTransactionOutDto(transaction);
            }
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    // Listar ativos


    // Converte transaction → transactionOutDto 
    private static TransactionOutDto MapToTransactionOutDto(Transaction transaction)
    {
        return new TransactionOutDto(
            transaction.Id,
            transaction.CustomerId,
            transaction.TransactionDate,
            transaction.TransactionType, 
            transaction.Quantity,
            transaction.UnitPrice,
            transaction.Ticker
        );
    }

    private static AssetOutDto MapToOutDto(
        Asset asset,
        decimal currentMarketPrice, 
        decimal totalCurrentValue,
        decimal returnPercentage,
        decimal profitOrLoss,
        bool isPriceUpToDate
    )
    {
        return new AssetOutDto(
            asset.Ticker,
            asset.Quantity,
            asset.AveragePrice,
            asset.CurrentAmountInvested,
            currentMarketPrice,
            totalCurrentValue,
            returnPercentage, 
            profitOrLoss, 
            isPriceUpToDate
            );
    }
}

