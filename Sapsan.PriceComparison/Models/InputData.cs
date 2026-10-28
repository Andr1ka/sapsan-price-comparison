namespace Sapsan.PriceComparison.Models;

public sealed class InputData
{
    public List<InvoiceItem> Invoice { get; set; } = [];

    public List<Offer> Offers { get; set; } = [];
}
