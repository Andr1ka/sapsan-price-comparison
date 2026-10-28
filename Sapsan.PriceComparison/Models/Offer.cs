namespace Sapsan.PriceComparison.Models;

public sealed class Offer
{
    public string Supplier { get; set; } = string.Empty;

    public string Sku { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public int Stock { get; set; }
}
