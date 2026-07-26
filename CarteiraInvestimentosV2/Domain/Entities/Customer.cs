using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarteiraInvestimentosV2.Domain.Entities;

public class Customer(string name, string email)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; set; } = name;
    public string? Email { get; set; } = email;
    public bool IsActive { get; private set; } = true;

    private readonly List<Domain.Entities.Asset> _assets = new(); // sem permissão a se alterar a lista original
    
    public IReadOnlyCollection<Domain.Entities.Asset> Assets => _assets.AsReadOnly(); // lista inalterável pública

    public void AddAsset(Domain.Entities.Asset asset)
    {
        var existingAsset = _assets.Find(a => a.Ticker == asset.Ticker);
        if (existingAsset is null)
        {
            _assets.Add(asset);
            return;
        }
        existingAsset.RegisterBuy(asset.Quantity, asset.AveragePrice);
    }

    public void SellAsset(Domain.Entities.Asset asset)
    {
        var teste = _assets.Find(a => a.Ticker == asset.Ticker);
        
    }

    public void InactivateAccount()
    {
        // Tratar as condições para inativação 
        IsActive = false;
    }
    
    public void ActivateAccount()
    {
        // Tratar as condições para inativação 
        IsActive = true;
    }
}