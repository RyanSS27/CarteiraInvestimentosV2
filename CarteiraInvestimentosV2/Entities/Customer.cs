using System.ComponentModel.DataAnnotations;

namespace CarteiraInvestimentosV2.Entities;

public class Customer
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    [Required]
    public string Name { get; set; }
    public string Email { get; set; }

    private readonly List<Asset> _assets = new(); // sem permissão a se alterar a lista original
    
    public IReadOnlyCollection<Asset> Assets => _assets.AsReadOnly(); // lista inalterável pública
    
    public List<Transaction> Transactions { get; private set; }

    public void AddAsset(Asset asset)
    {
        _assets.Add(asset);
        // implementar futuramente a lógica
    }

    public void SellAsset(Asset asset)
    {
        // implementar futuramente a lógica
    }

    public void AddTransaction(Transaction transaction)
    {
        Transactions.Add(transaction);
        // implementar futuramente a lógica
    }
}