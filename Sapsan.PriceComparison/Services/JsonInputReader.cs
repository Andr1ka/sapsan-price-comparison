using System.Text.Json;
using Sapsan.PriceComparison.Exceptions;
using Sapsan.PriceComparison.Interfaces;
using Sapsan.PriceComparison.Models;

namespace Sapsan.PriceComparison.Services;

public sealed class JsonInputReader : IInputReader
{
    private readonly string _path;

    public JsonInputReader(string path)
    {
        _path = path;
    }

    public InputData Read()
    {
        if (!File.Exists(_path))
        {
            throw new FileNotFoundException($"Файл входных данных не найден: {_path}", _path);
        }

        var json = File.ReadAllText(_path);
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

        var data = JsonSerializer.Deserialize<InputData>(json, options);
        if (data is null)
        {
            throw new InputDataNullException($"Не удалось прочитать данные из '{_path}'.");
        }
        

        return data;
    }
}
