using System.Text.RegularExpressions;
using CarteiraInvestimentosV2.Domain.Exceptions;
using CarteiraInvestimentosV2.Entities.Enums;

namespace CarteiraInvestimentosV2.Domain.Entities;

public partial class Transaction
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid CustomerId { get; private set; }
    public DateTime TransactionDate { get; private set; } = DateTime.UtcNow;

    public TransactionType TransactionType { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; } // quanto a ação valia no momento
    public string Ticker { get; private set; }
    
    public Transaction(
        Guid customerId,
        TransactionType transactionType,
        int quantity,
        decimal unitPrice,
        string ticker)
    {
        if (quantity <= 0)
            throw new ArgumentException("A quantidade de cotas deve ser maior que zero.");

        if (unitPrice <= 0)
            throw new ArgumentException("O preço unitário de compra deve ser maior que zero.");
        
        if (string.IsNullOrWhiteSpace(ticker) || !MyRegex().IsMatch(ticker))
            throw new ArgumentException("O Ticker informado é inválido. Padrão esperado: 4 letras seguidas de 1 ou 2 números.");
        
        TransactionType = transactionType;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Ticker = ticker.Trim().ToUpper();
    }
    
    [GeneratedRegex(@"^[a-zA-Z]{4}\d{1,2}$")]
    private static partial Regex MyRegex();
}