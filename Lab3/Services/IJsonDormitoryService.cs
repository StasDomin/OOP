using DormitoryJsonManager.Models;

namespace DormitoryJsonManager.Services;

public interface IJsonDormitoryService
{
    Task<DormitoryData> LoadFromFileAsync(string filePath);
    Task SaveToFileAsync(string filePath, DormitoryData data);
}
