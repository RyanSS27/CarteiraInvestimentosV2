using CarteiraInvestimentosV2.Entities.Enums;

namespace CarteiraInvestimentosV2.Dtos;

public record TransactionOutDto(
    Guid Id,
    Guid CustomerId,
    DateTime TransactionDate,
    TransactionType TransactionType,
    int Quantity,
    decimal UnitPrice,
    string Ticker
);