using CarteiraInvestimentosAPI.Entities.Enums;

namespace CarteiraInvestimentosAPI.Dtos;

public record TransactionOutDto(
    Guid Id,
    Guid CustomerId,
    DateTime TransactionDate,
    TransactionType TransactionType,
    int Quantity,
    decimal UnitPrice,
    string Ticker
);