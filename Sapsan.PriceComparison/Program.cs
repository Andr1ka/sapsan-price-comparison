using System.Globalization;
using System.Text;
using System.Text.Json;
using Sapsan.PriceComparison.Exceptions;
using Sapsan.PriceComparison.Interfaces;
using Sapsan.PriceComparison.Services;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");

try
{
    IInputReader reader = new JsonInputReader(
        Path.Combine(AppContext.BaseDirectory, "Data", "input.json"));

    var data = reader.Read();
    InputValidator.Validate(data);

    var recommendations = new ComparisonService().Analyze(data.Invoice, data.Offers);

    Console.WriteLine(
        $"{"Артикул",-8} {"Кол-во",-8} {"Цена счёта",-12} {"Поставщик",-20} {"Рек. цена",-12} {"Экономия",-10}");

    foreach (var item in recommendations)
    {
        Console.WriteLine(
            $"{item.Sku,-8} {item.Quantity,-8} {item.OriginalUnitPrice,-12:N2} {item.RecommendedSupplier,-20} {item.RecommendedUnitPrice,-12:N2} {item.Economy,-10:N2}");
    }

    Console.WriteLine();
    Console.WriteLine($"Суммарная потенциальная экономия: {recommendations.Sum(x => x.Economy):N2}");
    return 0;
}
catch (Exception ex) when (
    ex is FileNotFoundException
    or InputDataNullException
    or InvalidInputException
    or JsonException)
{
    Console.Error.WriteLine(ex.Message);
    return 1;
}
