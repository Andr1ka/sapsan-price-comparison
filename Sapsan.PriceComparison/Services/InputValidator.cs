using Sapsan.PriceComparison.Exceptions;
using Sapsan.PriceComparison.Models;

namespace Sapsan.PriceComparison.Services;

public static class InputValidator
{
    public static void Validate(InputData data)
    {
        if (data.Invoice is null || data.Invoice.Count == 0)
        {
            throw new InvalidInputException("Счёт пустой.");
        }

        data.Offers ??= [];

        for (var i = 0; i < data.Invoice.Count; i++)
        {
            var item = data.Invoice[i];
            if (string.IsNullOrWhiteSpace(item.Sku))
            {
                throw new InvalidInputException($"invoice[{i}]: пустой sku.");
            }

            if (item.Quantity <= 0)
            {
                throw new InvalidInputException($"invoice[{i}] ({item.Sku}): quantity должно быть > 0.");
            }

            if (item.UnitPrice < 0)
            {
                throw new InvalidInputException($"invoice[{i}] ({item.Sku}): цена не может быть отрицательной.");
            }
        }

        for (var i = 0; i < data.Offers.Count; i++)
        {
            var offer = data.Offers[i];
            if (string.IsNullOrWhiteSpace(offer.Supplier))
            {
                throw new InvalidInputException($"offers[{i}]: пустой поставщик.");
            }

            if (string.IsNullOrWhiteSpace(offer.Sku))
            {
                throw new InvalidInputException($"offers[{i}]: пустой sku.");
            }

            if (offer.Stock < 0)
            {
                throw new InvalidInputException($"offers[{i}] ({offer.Sku}): stock не может быть отрицательным.");
            }

            if (offer.UnitPrice < 0)
            {
                throw new InvalidInputException($"offers[{i}] ({offer.Sku}): цена не может быть отрицательной.");
            }
        }
    }
}
