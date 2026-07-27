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

    public async Task<List<AssetOutDto>> GetWalletSummary(Guid customerId)
    {
        var customer = await customerRepository.GetCustomerAsync(customerId);
        if (customer is null)
            throw new NotFoundException($"Cliente de id '{customerId}' não encontrado");

        if (customer.Assets.Count <= 0)
            throw new DomainException($"Cliente {customer.Name} de id {customerId} não possui investimentos em ativos no momento."); // odiei essa frase kkkkkkkkk

        List<AssetOutDto> assetsOut = [];
        
        foreach (var asset in customer.Assets)
        {
            decimal currentMarketPrice; // guardaria o valor atual do ativo no mercado 
            decimal totalCurrentValue; // currentMarketPrice x asset.Quantity
            decimal profitOrLoss; // totalCurrentValue - asset.CurrentAmountInvested
            decimal returnPercentage; //  ( profitOrLoss * 100 ) / asset.CurrentAmountInvested   
            bool isPriceUpToDate;
            try
            {
                // aqui vem a conexão com o FinancialMarketService (conexão com a Brapi)
                throw new NotImplementedException();
            }
            catch
            {
                currentMarketPrice = asset.AveragePrice;
                totalCurrentValue = asset.CurrentAmountInvested;
                profitOrLoss = 0;
                returnPercentage = 0;
                isPriceUpToDate = false;
                
                assetsOut.Add(MapToOutDto(
                    asset,
                    currentMarketPrice,
                    totalCurrentValue,
                    returnPercentage,
                    profitOrLoss,
                    isPriceUpToDate
                    ));
            }
        }

        return assetsOut;
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

