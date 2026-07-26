using System.ComponentModel.DataAnnotations;
using CarteiraInvestimentosV2.Domain.Exceptions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarteiraInvestimentosV2.Domain.Entities;

public class Customer
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; private set; } = true;

    private List<Asset> _assets = []; // sem permissão a se alterar a lista original
    
    public IReadOnlyCollection<Asset> Assets => _assets.AsReadOnly(); // lista inalterável pública

    public Customer(string name, string email)
    {
        Name = name;
        Email = email;
    }
    public void AddAsset(Asset asset)
    {
        if (_assets is null)
            _assets = [];
        
        var existingAsset = _assets.Find(a => a.Ticker == asset.Ticker);
        if (existingAsset is null)
        {
            _assets.Add(asset);
            return;
        }
        existingAsset.RegisterBuy(asset.Quantity, asset.AveragePrice);
    }

    public void SellAsset(Asset asset)
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