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
    private readonly int _limitPerRequest = 25;

    public async Task<List<TransactionOutDto>> ListCustomerTransactionAsync(Guid customerId, int limit)
    {
        var customer = await customerRepository.GetCustomerAsync(customerId);
        if (customer is null)
            throw new NotFoundException($"Cliente de id '{customerId}' não encontrado");
        
        if (limit > _limitPerRequest || limit <= 0)
            limit = _limitPerRequest;
        
        var transaction = await transactionRepository.ListTransactionsAsync(customerId, limit);
        return transaction
            .Select(MapToTransactionOutDto) // versão implicita que passa cada transação para a função
            .ToList();
    }

    public async Task<TransactionOutDto> RecordTransactionAsync(Guid customerId, TransactionInputDto transactionInput)
    {
        // diversão começa aqui
        if (transactionInput.TransactionType == TransactionType.BUY)
        {
            var customer = await customerRepository.GetCustomerAsync(customerId);
            if (customer is null)
                throw new NotFoundException($"Cliente de id '{customerId}' não encontrado");
            
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
            // por algum motivo o CustomerId da transaction está zerado 000000-00000... corrija
            await customerRepository.UpdateCustomerAsync(customer);
            await transactionRepository.AddTransactionAsync(transaction);
            
            return MapToTransactionOutDto(transaction);
        }
        else
        {
            throw new NotImplementedException();   
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

