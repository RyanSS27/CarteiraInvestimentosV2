using CarteiraInvestimentosV2.Dtos;
using CarteiraInvestimentosV2.Entities.Enums;

namespace CarteiraInvestimentosV2.Domain.Services.Ports;

public interface IWalletService
{
    Task<List<TransactionOutDto>> ListCustomerTransactionAsync(Guid customerId, int limit);
    Task<TransactionOutDto> RecordTransactionAsync(Guid customerId, TransactionInputDto transactionInput);

    // listar assets 
    Task<List<AssetOutDto>> GetWalletSummary(Guid customerId);
}