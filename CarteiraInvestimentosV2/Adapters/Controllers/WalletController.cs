using CarteiraInvestimentosV2.Domain.Services.Ports;
using CarteiraInvestimentosV2.Entities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraInvestimentosV2.Adapters.Controllers;

[Route("api/wallet")]
[ApiController]
public class WalletController(IWalletService walletService) : ControllerBase
{
    private readonly IWalletService _walletService = walletService;

    [HttpPost("{customerId:guid}")]
    public async Task<IActionResult> RecordTransaction(Guid customerId, TransactionInputDto transactionInput)
    {
        var transaction = await _walletService.RecordTransactionAsync(customerId, transactionInput);
        return Ok(transaction);
    }
}