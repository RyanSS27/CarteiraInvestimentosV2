using CarteiraInvestimentosV2.Domain.Services.Ports;
using CarteiraInvestimentosV2.Dtos;
using CarteiraInvestimentosV2.Entities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraInvestimentosV2.Adapters.Controllers;

[Route("api/wallet")]
[ApiController]
public class WalletController(IWalletService walletService) : ControllerBase
{
    private readonly IWalletService _walletService = walletService;

    [HttpPost("{customerId:guid}/transactions")]
    public async Task<IActionResult> RecordTransaction(Guid customerId, TransactionInputDto transactionInput)
    {
        var transaction = await _walletService.RecordTransactionAsync(customerId, transactionInput);
        return Ok(transaction);
    }

    [HttpGet("{customerId:guid}/transactions")]  
    public async Task<IActionResult> ListCustomerTransactions(Guid customerId, int limit)
    {
        return Ok(await _walletService.ListCustomerTransactionAsync(customerId, limit));
    }

    [HttpGet("{customerId:guid}/summary")]
    public async Task<IActionResult> GetWalletSummary(Guid customerId)
    {
        return Ok(await _walletService.GetWalletSummary(customerId));
    }
}