using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarteiraInvestimentosV2.Entities;

public class Customer
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; private set; } = true;

    private readonly List<Asset> _assets = new(); // sem permissão a se alterar a lista original
    
    public IReadOnlyCollection<Asset> Assets => _assets.AsReadOnly(); // lista inalterável pública
    
    private readonly List<Transaction> _transactions = new();
    private IReadOnlyCollection<Transaction> Transactions => _transactions;

    public Customer (string name, string email)
    {
        Name = name;
        Email = email;
    }

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
        _transactions.Add(transaction);
        // implementar futuramente a lógica
    }

    public void InactivateAccount()
    {
        // Tratar as condições para inativação 
        IsActive = false;
    }
}