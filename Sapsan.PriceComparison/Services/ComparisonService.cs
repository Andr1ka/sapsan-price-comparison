using Sapsan.PriceComparison.Models;

namespace Sapsan.PriceComparison.Services;

public sealed class ComparisonService
{
    private const string KeepCurrentSupplier = "оставить текущего";

    public List<Recommendation> Analyze(List<InvoiceItem> invoiceItems, List<Offer> offers)
    {
        var recommendations = new List<Recommendation>();

        foreach (var item in invoiceItems)
        {
            var bestOffer = offers
                .Where(offer => string.Equals(offer.Sku, item.Sku, StringComparison.InvariantCultureIgnoreCase))
                .Where(offer => offer.Stock >= item.Quantity)
                .Where(offer => offer.UnitPrice < item.UnitPrice)
                .OrderBy(offer => offer.UnitPrice)
                .FirstOrDefault();

            recommendations.Add(new Recommendation
            {
                Sku = item.Sku,
                Quantity = item.Quantity,
                OriginalUnitPrice = item.UnitPrice,
                RecommendedUnitPrice = bestOffer?.UnitPrice ?? item.UnitPrice,
                RecommendedSupplier = bestOffer?.Supplier ?? KeepCurrentSupplier,
                Economy = bestOffer is null
                    ? 0
                    : (item.UnitPrice - bestOffer.UnitPrice) * item.Quantity
            });
        }

        return recommendations;
    }
}
