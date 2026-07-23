using CarteiraInvestimentosV2.Database;
using CarteiraInvestimentosV2.Entities;

namespace CarteiraInvestimentosV2.Services;

public class WalletService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ITransactionRepository _transactionRepository;

    public WalletService(ICustomerRepository customerRepository, ITransactionRepository transactionRepository)
    {
        _customerRepository = customerRepository;
        _transactionRepository = transactionRepository;
    }
    
    
    
    
    // Converte transaction -> transactionOutDto 
    private TransactionOutDto MapToDto(Transaction transaction)
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

