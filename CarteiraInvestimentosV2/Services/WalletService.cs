using CarteiraInvestimentosV2.Database;

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
    
}