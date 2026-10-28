namespace Sapsan.PriceComparison.Models;

public sealed class Recommendation
{
    public string Sku { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal OriginalUnitPrice { get; set; }

    public decimal RecommendedUnitPrice { get; set; }

    public decimal Economy { get; set; }

    public string RecommendedSupplier { get; set; } = string.Empty;
}
