using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using DormitoryJsonManager.Models;

namespace DormitoryJsonManager.Services;

public class JsonDormitoryService : IJsonDormitoryService
{
    private readonly JsonSerializerOptions _serializerOptions;

    public JsonDormitoryService()
    {
        _serializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
        };
    }

    public async Task<DormitoryData> LoadFromFileAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            throw new FileNotFoundException("Обраний JSON-файл не знайдено.", filePath);
        }

        await using FileStream stream = File.OpenRead(filePath);
        DormitoryData? data = await JsonSerializer.DeserializeAsync<DormitoryData>(stream, _serializerOptions);

        return data ?? new DormitoryData();
    }

    public async Task SaveToFileAsync(string filePath, DormitoryData data)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("Шлях до файлу не може бути порожнім.", nameof(filePath));
        }

        await using FileStream stream = File.Create(filePath);
        await JsonSerializer.SerializeAsync(stream, data, _serializerOptions);
    }
}
