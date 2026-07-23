using System.ComponentModel.DataAnnotations;

namespace CarteiraInvestimentosV2.Entities;

public class Asset(string ticker, int quantity, decimal averagePrice)
{
    public string Ticker { get; private set; } = ticker;

    public int Quantity { get; private set; } = quantity;

    public decimal  AveragePrice { get; private set; } = averagePrice; 
    /*
        Valor atribuido direto na 1° compra e nas próximas transações carrega a média do valor 
        pago por aquela ação
    */

    public decimal CurrentAmountInvested => Quantity * AveragePrice;

    private decimal TotalAcquisitionCost { get; set; } = quantity * averagePrice; // acumula o valor total gasto na compra dessas ações

    public bool RegisterBuy(int quantity, decimal unitPrice)
    {
        if (unitPrice <= 0 || quantity <= 0)
            return false;

        TotalAcquisitionCost += quantity * unitPrice;
        
        Quantity += quantity;
        AveragePrice = Math.Round(TotalAcquisitionCost / Quantity, 2);
        return true;
    }

    public bool RegisterSell(int quantity)
    {
        if (quantity <= 0 || quantity > Quantity)
            return false;
        
        Quantity -= quantity;
        TotalAcquisitionCost = Quantity * AveragePrice;
        
        return true;
    }
}