namespace Sapsan.PriceComparison.Models;

public sealed class InvoiceItem
{
    public string Sku { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}
