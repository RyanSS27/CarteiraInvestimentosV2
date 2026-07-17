using System.ComponentModel.DataAnnotations.Schema;
using CarteiraInvestimentosV2.Entities.Enums;

namespace CarteiraInvestimentosV2.Entities;

public class Transaction
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid CustomerId { get; private set; }
    public DateTime TransactionDate { get; private set; }
    
    public TransactionType TransactionType { get; private set; }
    public int Quantity { get; private set; } 
    public double UnitPrice { get; private set; } // quanto a ação valia no momento
    public string Ticker { get; private set; }



