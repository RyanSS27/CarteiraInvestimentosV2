using System.ComponentModel.DataAnnotations;

namespace CarteiraInvestimentosV2.Entities;

public class Asset
{
    public string Ticker { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal  AveragePrice { get; private set; } // média do valor paguei por aquela ação

    public decimal TotalInvestedValue => Quantity * AveragePrice;
    
    // falta o construtor que recebe o preco base
    
    public bool RegisterBuy(int quantity, decimal amountInvested)
    {
        if (amountInvested <= 0 && quantity <= 0)
            return false;

        Quantity += quantity;
        AveragePrice = Math.Round(amountInvested / Quantity, 2);
        return true;
    }

    public bool RegisterSell(int quantity)
    {
        if (quantity <= 0 && quantity <= Quantity)
            return false;
        
        Quantity -= quantity;
        return true;
    }
}