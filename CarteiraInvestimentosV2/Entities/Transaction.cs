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



    // somar as vendas
    /*
     LÓGICA DAS VENDAS
        para saber o total de valor investido em uma ação, eu devo:
        1° varrer as transações
        2° somar as compras com base no ticker
        3° somar as vendas
        
        após, eu devo:
        
        4° assionar a função de venda do asset, atualizando apenas a sua quantidade,
        extraindo a mesma e calculando quantidade x averegePrice = amountInvested
        5° o valor médio gasto por ação não é alterado na entidade asset (seu averagePrice)
            com base na compra, mantendo o valor original da compra
        Por estar sem conexão com a Brapi, o fluxo será:
            (tenho intenção de deixar em aberto o try que verifica se a conexão funcionou para
            implementação futura) 
        
            6° como (por enquanto) não tenho a conexão com a brapi, será atribuido ao 
                preçoAtualMercado o valor valorMedioInvestido, zerando o lucroOuPrejuizo e a 
                porcentagemRetorno. E o valorTotalInvestido é atribuido o amountInvested
            [Precision(18, 2)] 
    */
}