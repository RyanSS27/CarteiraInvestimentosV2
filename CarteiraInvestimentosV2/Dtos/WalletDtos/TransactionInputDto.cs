using System.ComponentModel.DataAnnotations;

namespace CarteiraInvestimentosV2.Entities.Enums;

public record TransactionInputDto(
    [Required(ErrorMessage = "O Ticker é obrigatório.")]
    [RegularExpression(@"^[a-zA-Z]{4}\d{1,2}$", ErrorMessage = "O Ticker deve seguir o formato da B3 (ex: PETR4).")]
    string Ticker,
    
    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero.")]
    int Quantity,
    
    [Required(ErrorMessage = "O preço unitário é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço unitário deve ser maior que zero.")]
    decimal UnitPrice,
    [Required(ErrorMessage = "O tipo da transação é obrigatório. Valores válidos: 'BUY' / 'SELL'.")]
    TransactionType TransactionType
    );