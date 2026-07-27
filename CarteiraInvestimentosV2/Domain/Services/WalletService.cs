using CarteiraInvestimentosV2.Adapters.Infrastructure.Repositories;
using CarteiraInvestimentosV2.Database;
using CarteiraInvestimentosV2.Domain.Entities;
using CarteiraInvestimentosV2.Domain.Exceptions;
using CarteiraInvestimentosV2.Domain.Services.Ports;
using CarteiraInvestimentosV2.Entities.Enums;
using Asset = CarteiraInvestimentosV2.Domain.Entities.Asset;

namespace CarteiraInvestimentosV2.Domain.Services;

public class WalletService(ICustomerRepository customerRepository, ITransactionRepository transactionRepository)
    : IWalletService
{
    private const int LimitPerRequest = 25;

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
}

