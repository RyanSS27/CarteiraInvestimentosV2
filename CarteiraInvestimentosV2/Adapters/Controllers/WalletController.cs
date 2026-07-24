using CarteiraInvestimentosV2.Entities.Enums;
using CarteiraInvestimentosV2.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraInvestimentosV2.Controllers;

[Route("api/wallet")]
[ApiController]
public class WalletController(IWalletService walletService) : ControllerBase
{
    private readonly IWalletService _walletService = walletService;

    [HttpPost("{id:guid}")]
    public async Task<IActionResult> RecordTransaction(Guid id, TransactionInputDto transactionInput)
    {
        var transaction = await _walletService.RecordTransactionAsync(id, transactionInput);
        return Ok(transaction);
    }
}