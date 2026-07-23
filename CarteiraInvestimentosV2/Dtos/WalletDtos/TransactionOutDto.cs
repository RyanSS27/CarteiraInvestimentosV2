using CarteiraInvestimentosV2.Entities.Enums;

public record TransactionOutDto(
    Guid Id,
    Guid CustomerId,
    DateTime TransactionDate,
    TransactionType TransactionType,
    int Quantity,
    decimal UnitPrice,
    string Ticker
);